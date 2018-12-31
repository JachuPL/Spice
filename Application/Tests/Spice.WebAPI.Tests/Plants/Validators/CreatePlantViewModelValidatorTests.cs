using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Plants;
using Spice.ViewModels.Plants.Validators;

namespace Spice.WebAPI.Tests.Plants.Validators
{
    [TestFixture]
    internal sealed class CreatePlantViewModelValidatorTests
    {
        private CreatePlantViewModelValidator Validator;

        [SetUp]
        public void SetUp()
        {
            Validator = new CreatePlantViewModelValidator();
        }

        [TestCase(" \n\r\t", TestName = "Create Plant Validator should have error for whitespace name")]
        [TestCase(null, TestName = "Create Plant Validator should have error for null name")]
        public void ValidatorShouldHaveErrorWhenNameIsIncorrect(string name)
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Create Plant Validator should have error for empty name")]
        public void ValidatorShouldHaveErrorWhenNameIsEmpty()
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [TestCase(1, TestName = "Create Plant Validator should have error for too short name")]
        [TestCase(51, TestName = "Create Plant Validator should have error for too long name")]
        public void ValidatorShouldHaveErrorWhenNameIsTooShortOrTooLong(int charCount)
        {
            string name = string.Empty;
            for (int i = 0; i < charCount; i++)
                name += "a";

            Validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Create Plant Validator should not have error for correct name")]
        public void ValidatorShouldNotHaveErrorWhenNameIsSpecified()
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Capsicum annuum");
        }

        [TestCase(PlantStateViewModel.Healthy, TestName = "Create Plant Validator should not have error for Healty state")]
        [TestCase(PlantStateViewModel.Deceased, TestName = "Create Plant Validator should not have error for Deceased state")]
        [TestCase(PlantStateViewModel.Flowering, TestName = "Create Plant Validator should not have error for Flowering state")]
        [TestCase(PlantStateViewModel.Fruiting, TestName = "Create Plant Validator should not have error for Fruiting state")]
        [TestCase(PlantStateViewModel.Harvested, TestName = "Create Plant Validator should not have error for Harvested state")]
        [TestCase(PlantStateViewModel.Sick, TestName = "Create Plant Validator should not have error for Sick state")]
        public void ValidatorShouldNotHaveErrorWhenStateIsValid(PlantStateViewModel value)
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.State, value);
        }
    }
}