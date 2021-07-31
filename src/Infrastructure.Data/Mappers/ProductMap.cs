namespace Infrastructure.Data.Mappers
{
    using Domain.Orders;
    using FluentNHibernate.Mapping;

    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.ProductId).CustomType<string>().Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.Price).CustomType<decimal>().Access.CamelCaseField(Prefix.Underscore);
        }
    }
}