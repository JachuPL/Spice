using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Plants.Nutrients.Validators;

namespace Spice.WebAPI.Tests.Plants.Nutrients.Validators
{
    [TestFixture]
    internal sealed class CreateAdministeredNutrientViewModelValidatorTests
    {
        private CreateAdministeredNutrientViewModelValidator Validator;

        [SetUp]
        public void SetUp()
        {
            Validator = new CreateAdministeredNutrientViewModelValidator();
        }

        [TestCase(TestName = "Create Administered Nutrient Validator should have error for Amount equal to 0")]
        public void ValidatorShouldHaveErrorWhenAmountIsZero()
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Amount, 0);
        }
    }
}