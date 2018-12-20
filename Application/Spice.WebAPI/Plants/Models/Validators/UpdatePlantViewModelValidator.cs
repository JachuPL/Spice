using FluentValidation;

namespace Spice.WebAPI.Plants.Models.Validators
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

            RuleFor(x => x.FieldName)
                .NotEmpty().WithMessage("Filed name cannot be empty.")
                .MaximumLength(50).WithMessage("Maximum lenght of field name is 50 characters.");

            RuleFor(x => x.Planted)
                .NotNull().WithMessage("Plant date cannot be null.");

            RuleFor(x => x.State)
                .Must(BeAValidValue).WithMessage("Select a valid state of plant.");
        }

        private bool BeAValidValue(PlantStateViewModelEnum state)
        {
            switch (state)
            {
                case PlantStateViewModelEnum.Healthy:
                case PlantStateViewModelEnum.Deceased:
                case PlantStateViewModelEnum.Flowering:
                case PlantStateViewModelEnum.Fruiting:
                case PlantStateViewModelEnum.Harvested:
                case PlantStateViewModelEnum.Sick:
                    return true;

                default: return false;
            }
        }
    }
}