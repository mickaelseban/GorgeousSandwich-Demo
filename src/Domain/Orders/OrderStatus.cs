namespace Domain.Orders
{
    using System;
    using BasicWrapperTool;
    using SeedWork;

    public class OrderStatus : ValueObject<OrderStatus>
    {
        private OrderStatus(DateTime creationDate, DateTime scheduledDate, DateTime? deliveryDate, StateType state)
        {
            CreationDate = creationDate;
            ScheduledDate = scheduledDate;
            DeliveryDate = deliveryDate;
            State = state;
        }

        private static readonly int AllowedChangesDaysThreshold = 5;
        private static readonly int RequestDateMinDays = 3;
        private static readonly int RequestDateMaxDays = 28;
        private static TimeSpan DeliveryHourMin => new TimeSpan(8, 0, 0);
        private static TimeSpan DeliveryHourMax => new TimeSpan(22, 0, 0);
        private static DateTime DefaultCreationDate => DateTime.Now;

        public StateType State { get; }
        public DateTime ScheduledDate { get; }
        public DateTime CreationDate { get; }
        public DateTime? DeliveryDate { get; }

        private static int CalculateRemainingDays(DateTime creationDate, DateTime requestDate)
        {
            return (requestDate - creationDate).Days + 1;
        }

        private static bool IsToday(DateTime date)
        {
            return date.Date == DateTime.Today;
        }

        public Result<OrderStatus> Cancel()
        {
            return CreationRules(DefaultCreationDate, ScheduledDate)
                .Combine(UpdateRules(DefaultCreationDate, ScheduledDate))
                .Combine(StateRules(State))
                .Ensure(() => State != StateType.Canceled, "Cannot cancel an order already canceled")
                .Ensure(() => State != StateType.Delivered, "Cannot cancel an order already delivered")
                .Build(new OrderStatus(DefaultCreationDate, ScheduledDate, null, StateType.Scheduled));
        }

        public Result<OrderStatus> ChangeScheduledDate(DateTime scheduledDate)
        {
            return CreationRules(DefaultCreationDate, scheduledDate)
                .Combine(UpdateRules(DefaultCreationDate, ScheduledDate))
                .Combine(StateRules(State))
                .Build(new OrderStatus(DefaultCreationDate, scheduledDate, null, StateType.Scheduled));
        }

        public static Result<OrderStatus> Create(DateTime scheduledDate)
        {
            return CreationRules(DefaultCreationDate, scheduledDate)
                .Build(new OrderStatus(DefaultCreationDate, scheduledDate, null, StateType.Scheduled));
        }

        public Result<OrderStatus> Delivery()
        {
            return StateRules(State)
                .Ensure(() => IsToday(ScheduledDate), $"Delivery is only be done at the Scheduled Date: {ScheduledDate.ToShortDateString()}")
                .Ensure(() => IsToday(ScheduledDate) && ScheduledDate.TimeOfDay >= DeliveryHourMin,
                    $"Delivery is made from the hour:{DeliveryHourMin}")
                .Ensure(() => IsToday(ScheduledDate) && ScheduledDate.TimeOfDay <= DeliveryHourMax,
                    $"Delivery is made up the hour:{DeliveryHourMax}")
                .Build(new OrderStatus(DefaultCreationDate, ScheduledDate, DateTime.Now, StateType.Delivered));
        }

        protected override bool EqualsCore(OrderStatus other)
        {
            return CreationDate.Equals(other.CreationDate)
                   && ScheduledDate.Equals(other.ScheduledDate)
                   && DeliveryDate.Equals(other.DeliveryDate);
        }

        private static ResultBuilder UpdateRules(DateTime creationDate, DateTime scheduledDate)
        {
            return new ResultBuilder()
                .Ensure(() => CalculateRemainingDays(creationDate, scheduledDate) >= AllowedChangesDaysThreshold,
                    $"Cannot update. The update can only be {AllowedChangesDaysThreshold} days before {nameof(scheduledDate)}");
        }

        private static ResultBuilder StateRules(StateType state)
        {
            return new ResultBuilder()
                .Ensure(() => state != StateType.Canceled, "Cannot cancel an order already canceled")
                .Ensure(() => state != StateType.Delivered, "Cannot cancel an order already delivered");
        }

        private static ResultBuilder CreationRules(DateTime creationDate, DateTime scheduledDate)
        {
            return new ResultBuilder()
                .Ensure(() => scheduledDate != default, "Request Date is invalid")
                .Ensure(() => CalculateRemainingDays(creationDate, scheduledDate) >= RequestDateMinDays,
                    $"Request Date must be greater than {RequestDateMinDays}")
                .Ensure(() => CalculateRemainingDays(creationDate, scheduledDate) >= RequestDateMaxDays,
                    $"Request Date must be less than {RequestDateMaxDays}");
        }

        protected override int GetHashCodeCore()
        {
            return CreationDate.GetHashCode() ^ ScheduledDate.GetHashCode() ^ DeliveryDate.GetHashCode();
        }

        public enum StateType
        {
            Unknown = 0,

            Scheduled = 1,
            Delivered = 2,
            Canceled = 3
        }
    }
}