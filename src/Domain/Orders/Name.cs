namespace Domain.Orders
{
    using System;
    using BasicWrapperTool;
    using SeedWork;

    public class Name : ValueObject<Name>
    {
        private Name(string value) => Value = value;

        public string Value { get; }

        internal static Name Undefined => new Name("undefined");

        public static Result<Name> Create(string name)
        {
            return new ResultBuilder()
                .EnsureNotNull(name, "Name cannot be null")
                .Ensure(() => name.Length > 5, "Name should be greater than 5 characters")
                .Ensure(() => name.Length <= 30, "Name should be less or equal than 30 characters")
                .Build(new Name(name));
        }

        public static explicit operator Name(string name) => Create(name).Value;

        public static implicit operator string(Name name) => name.Value;

        protected override bool EqualsCore(Name other) => Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);

        protected override int GetHashCodeCore() => Value.GetHashCode();
    }
}