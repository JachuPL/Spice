using FluentValidation;
using System;

namespace Spice.ViewModels.Plants.Events.Validators
{
    public class CreatePlantEventViewModelValidator : AbstractValidator<CreatePlantEventViewModel>
    {
        public CreatePlantEventViewModelValidator()
        {
            RuleFor(x => x.Type)
                .Must(BeAValidValue).WithMessage("Select a valid type of event.");

            RuleFor(x => x.Description)
                .Must(BeAValidDescription).WithMessage("Description cannot be build from whitespace characters only.")
                .MinimumLength(2).WithMessage("Minimum length of event description is 2 characters.")
                .MaximumLength(500).WithMessage("Maximum lenght of event description is 500 characters.");

            RuleFor(x => x.Occured)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Event occurence date cannot be in the future");
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
                    return true;

                default:
                    return false;
            }
        }

        private bool BeAValidDescription(string arg)
        {
            if (arg is null)
                return true;

            return arg.Trim().Length > 0;
        }
    }
}