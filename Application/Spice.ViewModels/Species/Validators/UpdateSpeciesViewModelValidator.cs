using FluentValidation;
using Spice.ViewModels.Common.Validators;

namespace Spice.ViewModels.Species.Validators
{
    public class UpdateSpeciesViewModelValidator : AbstractValidator<UpdateSpeciesViewModel>
    {
        public UpdateSpeciesViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Species name cannot be empty.")
                .MinimumLength(2).WithMessage("Minimum length of species name is 2 characters.")
                .MaximumLength(50).WithMessage("Maximum lenght of species name is 50 characters.");

            RuleFor(x => x.LatinName)
                .NotEmpty().WithMessage("Species latin name cannot be empty.")
                .MinimumLength(2).WithMessage("Minimum length of species latin name is 2 characters.")
                .MaximumLength(50).WithMessage("Maximum lenght of species latin name is 50 characters.");

            RuleFor(x => x.Description)
                .SetValidator(new DescriptionViewModelValidator())
                .MinimumLength(2).WithMessage("Minimum length of species description is 2 characters.")
                .MaximumLength(500).WithMessage("Maximum length of species description is 500 characters.");
        }
    }
}