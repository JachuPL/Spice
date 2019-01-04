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

            RuleFor(x => x.SpeciesId)
                .NotEmpty().WithMessage("Plant species cannot be empty.");

            RuleFor(x => x.FieldId)
                .NotEmpty().WithMessage("Plant field cannot be empty.");

            RuleFor(x => x.Planted)
                .NotNull().WithMessage("Plant date cannot be null.");

            RuleFor(x => x.State)
                .SetValidator(new PlantStateViewModelValidator());
        }
    }
}