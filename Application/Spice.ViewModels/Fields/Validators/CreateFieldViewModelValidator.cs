using FluentValidation;

namespace Spice.ViewModels.Fields.Validators
{
    public class CreateFieldViewModelValidator : AbstractValidator<CreateFieldViewModel>
    {
        public CreateFieldViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Field name cannot be empty.")
                .MinimumLength(2).WithMessage("Minimum length of field name is 2 characters.")
                .MaximumLength(20).WithMessage("Maximum length of field name is 20 characters.");

            RuleFor(x => x.Description)
                .MinimumLength(5).WithMessage("Minimum length of field description is 5 characters.")
                .MaximumLength(500).WithMessage("Maximum length of field description is 500 characters.");

            RuleFor(x => x.Latitude)
                .GreaterThan(-90).WithMessage("Latitude cannot be less than -90 degrees.")
                .LessThan(90).WithMessage("Latitude cannot be greater than +90 degrees.");

            RuleFor(x => x.Longtitude)
                .GreaterThan(-180).WithMessage("Longtitude cannot be less than -180 degrees.")
                .LessThan(180).WithMessage("Longtitude cannot be greater than +180 degrees.");
        }
    }
}