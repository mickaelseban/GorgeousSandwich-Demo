namespace Domain.Orders
{
    public class Institution : Customer
    {
        public Institution(Id customerNumber, string name, string email, TaxNumber taxNumber)
            : base(customerNumber, name, email, taxNumber)
        {
        }

        public override CustomerType Type => CustomerType.Institution;
    }
}