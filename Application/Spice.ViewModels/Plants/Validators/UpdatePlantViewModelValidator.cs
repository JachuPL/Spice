using FluentValidation;

namespace Spice.ViewModels.Plants.Validators
{
    public class UpdatePlantViewModelValidator : AbstractValidator<UpdatePlantViewModel>
    {
        public UpdatePlantViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Plant name cannot be empty.")
                .MinimumLength(2).WithMessage("Minimum length of plant name is 2 characters.")
                .MaximumLength(50).WithMessage("Maximum lenght of plant name is 50 characters.");

            RuleFor(x => x.Specimen)
                .NotEmpty().WithMessage("Plant specimen cannot be empty.")
                .MinimumLength(2).WithMessage("Minimum length of plant specimen is 2 characters.")
                .MaximumLength(50).WithMessage("Maximum lenght of plant specimen is 50 characters.");

            RuleFor(x => x.FieldId)
                .NotEmpty().WithMessage("Field name cannot be empty.");

            RuleFor(x => x.Planted)
                .NotNull().WithMessage("Plant date cannot be null.");

            RuleFor(x => x.State)
                .Must(BeAValidValue).WithMessage("Select a valid state of plant.");
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
                    return true;

                default: return false;
            }
        }
    }
}