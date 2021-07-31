namespace API.Orders
{
    using System;

    public class UpdateScheduledDateDto 
    {
        public DateTime NewScheduleDate { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerId { get; set; }
    }
}