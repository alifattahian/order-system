using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    internal class AddOrderValidator : AbstractValidator<Order>
    {
        private readonly ITimeService _timeService;
        public AddOrderValidator(ITimeService timeService)
        {
            _timeService = timeService;

            RuleFor(order => order.NetPrice).GreaterThanOrEqualTo(50000).WithMessage("adding an order with amount less than 50000 is not permitted");

            RuleFor(order => order.CreateDateTime).Custom((dateTime, context) =>
            {

                DateTime tehranDateTime = _timeService.ConvertToLocalDateTime(dateTime);
                TimeSpan startTime = new TimeSpan(8, 0, 0); // 8:00 AM
                TimeSpan endTime = new TimeSpan(19, 0, 0);  // 7:00 PM
                if (tehranDateTime.TimeOfDay.CompareTo(startTime) < 0 || tehranDateTime.TimeOfDay.CompareTo(endTime) > 0)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure("CreateDateTime", "Order time should be between 8 AM and 7 PM."));
                }
            });
        }
    }
}
