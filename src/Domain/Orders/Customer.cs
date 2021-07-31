namespace Domain.Orders
{
    using SeedWork;

    public abstract class Customer : Entity
    {
        protected Customer(Id customerNumber, string name, string email, TaxNumber taxNumber)
        {
            CustomerNumber = customerNumber;
            Name = name;
            Email = email;
            TaxNumber = taxNumber;
        }

        private string _customerNumber;

        public Id CustomerNumber
        {
            get => (Id)_customerNumber;
            protected set => _customerNumber = value;
        }

        public string Name { get; }
        public string Email { get; }

        private string _taxNumber;

        public TaxNumber TaxNumber
        {
            get => (TaxNumber)_taxNumber;
            protected set => _taxNumber = value;
        }

        public abstract CustomerType Type { get; }

        public enum CustomerType
        {
            Student,
            Institution
        }
    }
}