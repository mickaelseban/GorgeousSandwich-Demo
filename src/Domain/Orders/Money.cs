namespace Domain.Orders
{
    using BasicWrapperTool;
    using SeedWork;

    public class Money : ValueObject<Money>
    {
        private const decimal MaxMoneyAmount = 1_000_000;

        public decimal Value { get; }

        public static readonly Money Zero = new Money(0);

        public bool IsZero => Value == Zero.Value;

        private Money(decimal value)
        {
            Value = value;
        }

        public static Result<Money> Create(decimal money)
        {
            return new ResultBuilder()
                .Ensure(() => money < 0, "Money amount cannot be negative")
                .Ensure(() => money > MaxMoneyAmount, "Money amount cannot be greater than " + MaxMoneyAmount)
                .Ensure(() => money % 0.01m > 0, "Money amount cannot contain part lower than cents")
                .Build(new Money(money));
        }

        public static explicit operator Money(decimal money)
        {
            return Create(money).Value;
        }

        public static Money operator *(Money money, decimal multiplier)
        {
            return new Money(money.Value * multiplier);
        }

        public static Money operator +(Money money1, Money money2)
        {
            return new Money(money1.Value + money2.Value);
        }

        protected override bool EqualsCore(Money other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }
    }
}