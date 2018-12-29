using FluentValidation;

namespace Spice.ViewModels.Plants.AdministeredNutrients.Validators
{
    public class UpdateAdministeredNutrientViewModelValidator : AbstractValidator<UpdateAdministeredNutrientViewModel>
    {
        public UpdateAdministeredNutrientViewModelValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Administered nutrient amount must be greater than 0.");

            RuleFor(x => x.NutrientId)
                .NotEmpty().WithMessage("Administered nutrient cannot be empty.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Administered nutrient date cannot be empty.");
        }
    }
}