using FluentValidation;

namespace Spice.ViewModels.Plants.AdministeredNutrients.Validators
{
    public class CreateAdministeredNutrientViewModelValidator : AbstractValidator<CreateAdministeredNutrientViewModel>
    {
        public CreateAdministeredNutrientViewModelValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Administered nutrient amount must be greater than 0.");

            RuleFor(x => x.NutrientId)
                .NotEmpty().WithMessage("Administered nutrient cannot be empty.");
        }
    }
}