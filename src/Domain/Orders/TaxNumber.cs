namespace Domain.Orders
{
    using System.Text.RegularExpressions;
    using BasicWrapperTool;
    using SeedWork;

    public class TaxNumber : ValueObject<TaxNumber>
    {
        private TaxNumber(string value) => Value = value;

        public string Value { get; }

        public static Result<TaxNumber> Create(string taxNumber)
        {
            return new ResultBuilder()
                .Ensure(() => taxNumber.Length == 9, "TaxNumber should should have 9 characters")
                .Ensure(() => Regex.IsMatch(taxNumber, @"[0-9]"), "TaxNumber should be from 0 to 9")
                .Build(new TaxNumber(taxNumber));
        }

        public static explicit operator TaxNumber(string taxNumber) => new TaxNumber(taxNumber);

        public static implicit operator string(TaxNumber taxNumber) => taxNumber.Value;

        protected override bool EqualsCore(TaxNumber other) => Value.Equals(other.Value);

        protected override int GetHashCodeCore() => Value.GetHashCode();
    }
}