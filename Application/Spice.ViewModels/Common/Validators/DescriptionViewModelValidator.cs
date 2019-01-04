using FluentValidation;

namespace Spice.ViewModels.Common.Validators
{
    public class DescriptionViewModelValidator : AbstractValidator<string>
    {
        public DescriptionViewModelValidator()
        {
            RuleFor(x => x).Must(BeAValidDescription)
                           .WithMessage("Description cannot be build from whitespace characters only.");
        }

        private bool BeAValidDescription(string arg)
        {
            return string.IsNullOrEmpty(arg) || (arg.Trim().Length > 0);
        }
    }
}