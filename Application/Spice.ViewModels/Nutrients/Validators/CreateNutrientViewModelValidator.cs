using FluentValidation;
using Spice.ViewModels.Common.Validators;

namespace Spice.ViewModels.Nutrients.Validators
{
    public class CreateNutrientViewModelValidator : AbstractValidator<CreateNutrientViewModel>
    {
        public CreateNutrientViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nutrient name cannot be empty.")
                .MinimumLength(2).WithMessage("Minimum length of nutrient name is 2 characters.")
                .MaximumLength(50).WithMessage("Maximum length of nutrient name is 50 characters.");

            RuleFor(x => x.Description)
                .SetValidator(new DescriptionViewModelValidator())
                .MinimumLength(5).WithMessage("Minimum length of nutrient description is 5 characters.")
                .MaximumLength(500).WithMessage("Maximum length of nutrient description is 500 characters.");

            RuleFor(x => x.DosageUnits)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Dosage unit cannot be build from whitespace characters only.")
                .NotEmpty().WithMessage("Nutrient dosage unit cannot be empty.")
                .MaximumLength(20).WithMessage("Maximum length of nutrient dosage unit is 20 characters.");
        }
    }
}