using FluentValidation;

namespace Spice.ViewModels.Plants.Validators
{
    public class PlantStateViewModelValidator : AbstractValidator<PlantStateViewModel>
    {
        public PlantStateViewModelValidator()
        {
            RuleFor(x => x)
                .Must(BeAValidValue).WithMessage("Select a valid state for plant");
        }

        private bool BeAValidValue(PlantStateViewModel state)
        {
            switch (state)
            {
                case PlantStateViewModel.Healthy:
                case PlantStateViewModel.Deceased:
                case PlantStateViewModel.Flowering:
                case PlantStateViewModel.Fruiting:
                case PlantStateViewModel.Harvested:
                case PlantStateViewModel.Sick:
                case PlantStateViewModel.Sprouting:
                    return true;

                default: return false;
            }
        }
    }
}