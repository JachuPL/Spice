using FluentValidation;
using Spice.ViewModels.Common.Validators;

namespace Spice.ViewModels.Fields.Validators
{
    public class UpdateFieldViewModelValidator : AbstractValidator<UpdateFieldViewModel>
    {
        public UpdateFieldViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Field name cannot be empty.")
                .MinimumLength(2).WithMessage("Minimum length of field name is 2 characters.")
                .MaximumLength(50).WithMessage("Maximum length of field name is 50 characters.");

            RuleFor(x => x.Description)
                .SetValidator(new DescriptionViewModelValidator())
                .MinimumLength(5).WithMessage("Minimum length of field description is 5 characters.")
                .MaximumLength(500).WithMessage("Maximum length of field description is 500 characters.");

            RuleFor(x => x.Latitude)
                .GreaterThanOrEqualTo(-90).WithMessage("Latitude cannot be less than -90 degrees.")
                .LessThanOrEqualTo(90).WithMessage("Latitude cannot be greater than +90 degrees.");

            RuleFor(x => x.Longtitude)
                .GreaterThanOrEqualTo(-180).WithMessage("Longtitude cannot be less than -180 degrees.")
                .LessThanOrEqualTo(180).WithMessage("Longtitude cannot be greater than +180 degrees.");
        }
    }
}