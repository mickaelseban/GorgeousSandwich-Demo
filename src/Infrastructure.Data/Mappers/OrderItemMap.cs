namespace Infrastructure.Data.Mappers
{
    using Domain.Orders;
    using FluentNHibernate.Mapping;

    public class OrderItemMap : ClassMap<OrderItem>
    {
        public OrderItemMap()
        {
            Id(x => x.Id);

            Map(x => x.Quantity);
            HasOne(x => x.Product);
        }
    }
}