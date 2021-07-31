namespace Domain.Orders
{
    using SeedWork;

    public class OrderItem : Entity
    {
        internal OrderItem(uint quantity, Product product)
        {
            Quantity = quantity;
            Product = product;
        }

        public Money Price => Product.Price * Quantity;

        public uint Quantity { get; }

        public Product Product { get; }
    }
}