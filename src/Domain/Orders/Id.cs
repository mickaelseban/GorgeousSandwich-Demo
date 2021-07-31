namespace Domain.Orders
{
    using System;
    using BasicWrapperTool;
    using SeedWork;

    public class Id : ValueObject<Id>
    {
        private Id(string value) => Value = value;

        private Id(Guid value) : this(value.ToString())
        {
        }

        public string Value { get; }

        public static Result<Id> Create(string value)
        {
            var canParse = Guid.TryParse(value, out var guid);

            return new ResultBuilder()
                .Ensure(() => canParse, "Id is in invalid format")
                .Build(new Id(guid));
        }

        public static Id Generate()
        {
            return new Id(Guid.NewGuid());
        }

        public static explicit operator Id(string value)
        {
            return new Id(Guid.Parse(value));
        }

        public static implicit operator string(Id value)
        {
            return value.Value;
        }

        protected override bool EqualsCore(Id other) => Value.Equals(other.Value);

        protected override int GetHashCodeCore() => Value.GetHashCode();
    }
}