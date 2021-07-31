namespace Domain.Orders
{
    public class Student : Customer
    {
        public Student(Id customerNumber, string name, string email, TaxNumber taxNumber) : base(customerNumber, name, email, taxNumber)
        {
        }

        public override CustomerType Type => CustomerType.Student;
    }
}