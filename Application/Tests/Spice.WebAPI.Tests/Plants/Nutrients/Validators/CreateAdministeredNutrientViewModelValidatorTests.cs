using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Plants.Nutrients.Validators;

namespace Spice.WebAPI.Tests.Plants.Nutrients.Validators
{
    [TestFixture]
    internal sealed class CreateAdministeredNutrientViewModelValidatorTests
    {
        private CreateAdministeredNutrientViewModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreateAdministeredNutrientViewModelValidator();
        }

        [TestCase(TestName = "Create Administered Nutrient Validator should have error for Amount equal to 0")]
        public void ValidatorShouldHaveErrorWhenAmountIsZero()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Amount, 0);
        }
    }
}