namespace Application.Commands
{
    using System;
    using BasicWrapperTool;
    using MediatR;

    public class UpdateScheduledDateCommand : IRequest<Result>
    {
        public UpdateScheduledDateCommand(string studentId, string orderNumber, DateTime newScheduleDate)
        {
            StudentId = studentId;
            OrderNumber = orderNumber;
            NewScheduleDate = newScheduleDate;
        }

        public string StudentId { get; }
        public string OrderNumber { get; }
        public DateTime NewScheduleDate { get; }
    }
}