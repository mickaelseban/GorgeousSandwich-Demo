namespace Domain.Orders
{
    using SeedWork;

    public sealed class Product : Entity
    {
        public Product(Id productId, Name name, string description, Money price)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Price = price;
        }

        public string Description { get; }

        private string _productId;
        public Id ProductId
        {
            get => (Id)_productId;
            protected set => _productId = value;
        }

        private string _name;
        public Name Name
        {
            get => (Name)_name;
            protected set => _name = value;
        }

        private decimal _price;
        public Money Price
        {
            get => (Money)_price;
            protected set => _price = value;
        }
    }
}