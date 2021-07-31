namespace Infrastructure.Data.Mappers
{
    using Domain.Orders;
    using FluentNHibernate;
    using FluentNHibernate.Mapping;

    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.Id);
            DiscriminateSubClassesOnColumn("CustomerType");

            Map(x => x.Name);
            Map(x => x.Email);
            Map(x => x.CustomerNumber).CustomType<string>().Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.TaxNumber).CustomType<string>().Access.CamelCaseField(Prefix.Underscore);

            Map(Reveal.Member<Customer>("CustomerType")).CustomType<int>();
        }

        public class StudentMap : SubclassMap<Student>
        {
            public StudentMap()
            {
                DiscriminatorValue(Customer.CustomerType.Student);
            }
        }

        public class InstitutionMap : SubclassMap<Institution>
        {
            public InstitutionMap()
            {
                DiscriminatorValue(Customer.CustomerType.Institution);
            }
        }
    }
}