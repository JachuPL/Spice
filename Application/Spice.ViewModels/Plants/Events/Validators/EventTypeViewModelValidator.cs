using FluentValidation;

namespace Spice.ViewModels.Plants.Events.Validators
{
    public class EventTypeViewModelValidator : AbstractValidator<EventTypeViewModel>
    {
        public EventTypeViewModelValidator()
        {
            RuleFor(x => x)
                .Must(BeAValidValue).WithMessage("Select a valid type of event.");
        }

        private bool BeAValidValue(EventTypeViewModel value)
        {
            switch (value)
            {
                case EventTypeViewModel.Disease:
                case EventTypeViewModel.Fungi:
                case EventTypeViewModel.Growth:
                case EventTypeViewModel.Insects:
                case EventTypeViewModel.OverWatering:
                case EventTypeViewModel.Pests:
                case EventTypeViewModel.UnderWatering:
                case EventTypeViewModel.Nutrition:
                case EventTypeViewModel.Sprouting:
                    return true;

                default:
                    return false;
            }
        }
    }
}