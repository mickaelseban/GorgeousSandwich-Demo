using Infrastructure.Data.Repositories;
using Infrastructure.Data.UnitOfWork;

namespace API
{
    using System.Reflection;
    using Application.Commands;
    using AutoMapper;
    using Domain.Orders;
    using Domain.SeedWork;
    using Infrastructure.CrossCutting;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseMiddleware<ExceptionHandler>();
            //app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Mediator
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<Order>), typeof(OrderRepository));
            services.AddSingleton(new DbConnectionString(Configuration["CommandsConnectionString"]));

            //Auto Mapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddSwaggerGen();

            services.AddMvc();
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrderBuyerCommand.Product, Product>();
            CreateMap<Product, CreateOrderBuyerCommand.Product>();
        }
    }
}