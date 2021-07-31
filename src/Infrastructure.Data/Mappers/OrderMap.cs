namespace Infrastructure.Data.Mappers
{
    using System;
    using Domain.Orders;
    using FluentNHibernate;
    using FluentNHibernate.Mapping;

    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Id(x => x.Id);

            DiscriminateSubClassesOnColumn("OrderType");
            Map(x => x.OrderNumber).CustomType<string>().Access.CamelCaseField(Prefix.Underscore);
            HasMany(x => x.OrderItems);

            Component(x => x.Status, y =>
            {
                y.Map(x => x.CreationDate, "CreationDate").CustomType<DateTime>();
                y.Map(x => x.ScheduledDate, "ScheduledDate").CustomType<DateTime>();
                y.Map(x => x.DeliveryDate, "DeliveryDate").CustomType<DateTime?>().Nullable();
            });

            Map(Reveal.Member<Order>("OrderType")).CustomType<int>();
        }
    }

    public class OrderBuyerMap : SubclassMap<OrderBuyer>
    {
        public OrderBuyerMap()
        {
            DiscriminatorValue(Order.OrderType.Buyer);
            HasOne(x => x.Student);
        }
    }

    public class InstitutionMap : SubclassMap<OrderCharity>
    {
        public InstitutionMap()
        {
            DiscriminatorValue(Order.OrderType.Charity);
            HasOne(x => x.Institution);
        }
    }
}