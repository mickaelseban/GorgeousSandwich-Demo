﻿namespace Infrastructure.Data.Session
{
    using System.Reflection;
    using CrossCutting;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.AcceptanceCriteria;
    using FluentNHibernate.Conventions.Helpers;
    using FluentNHibernate.Conventions.Instances;
    using FluentNHibernate.Mapping;
    using NHibernate;

    public sealed class SessionFactory
    {
        private readonly ISessionFactory _factory;

        public SessionFactory(DbConnectionString connectionString)
        {
            _factory = BuildSessionFactory(connectionString);
        }

        internal ISession OpenSession()
        {
            return _factory.OpenSession();
        }

        private static ISessionFactory BuildSessionFactory(DbConnectionString connectionString)
        {
            var configuration = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(connectionString.Value))
                .Mappings(m => m.FluentMappings
                    .AddFromAssembly(Assembly.GetExecutingAssembly())
                    .Conventions.Add(
                        ForeignKey.EndsWith("ID"),
                        ConventionBuilder.Property.When(criteria => criteria.Expect(x => x.Nullable, Is.Not.Set),
                            x => x.Not.Nullable()))
                    .Conventions.Add<OtherConversions>()
                    .Conventions.Add<TableNameConvention>()
                    .Conventions.Add<HiLoConvention>()
                );

            return configuration.BuildSessionFactory();
        }

        private class HiLoConvention : IIdConvention
        {
            public void Apply(IIdentityInstance instance)
            {
                instance.Column(instance.EntityType.Name + "ID");
                instance.GeneratedBy.Native();
            }
        }

        private class OtherConversions : IHasManyConvention, IReferenceConvention
        {
            public void Apply(IOneToManyCollectionInstance instance)
            {
                instance.LazyLoad();
                instance.AsBag();
                instance.Cascade.SaveUpdate();
                instance.Inverse();
            }

            public void Apply(IManyToOneInstance instance)
            {
                instance.LazyLoad(Laziness.Proxy);
                instance.Cascade.None();
                instance.Not.Nullable();
            }
        }

        private class TableNameConvention : IClassConvention
        {
            public void Apply(IClassInstance instance)
            {
                instance.Table(instance.EntityType.Name);
            }
        }
    }
}