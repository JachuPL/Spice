using FluentValidation;
using Spice.ViewModels.Common.Validators;
using System;

namespace Spice.ViewModels.Plants.Events.Validators
{
    public class UpdatePlantEventViewModelValidator : AbstractValidator<UpdatePlantEventViewModel>
    {
        public UpdatePlantEventViewModelValidator()
        {
            RuleFor(x => x.Type)
                .SetValidator(new EventTypeViewModelValidator());

            RuleFor(x => x.Description)
                .SetValidator(new DescriptionViewModelValidator())
                .MinimumLength(2).WithMessage("Minimum length of event description is 2 characters.")
                .MaximumLength(500).WithMessage("Maximum lenght of event description is 500 characters.");

            RuleFor(x => x.Occured)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Event occurence date cannot be in the future");
        }
    }
}