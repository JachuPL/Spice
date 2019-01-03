using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Nutrients.Validators;
using Spice.WebAPI.Tests.Common;

namespace Spice.WebAPI.Tests.Nutrients.Validators
{
    [TestFixture]
    internal sealed class UpdateNutrientViewModelValidatorTests
    {
        private UpdateNutrientViewModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UpdateNutrientViewModelValidator();
        }

        [TestCase(" \n\r\t", TestName = "Update Nutrient Validator should have error for whitespace name")]
        [TestCase(null, TestName = "Update Nutrient Validator should have error for null name")]
        public void ValidatorShouldHaveErrorWhenNameIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Update Nutrient Validator should have error for empty name")]
        public void ValidatorShouldHaveErrorWhenNameIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [TestCase(1, TestName = "Update Nutrient Validator should have error for too short name")]
        [TestCase(51, TestName = "Update Nutrient Validator should have error for too long name")]
        public void ValidatorShouldHaveErrorWhenNameIsTooShortOrTooLong(int charCount)
        {
            string name = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Update Nutrient Validator should not have error for correct name")]
        public void ValidatorShouldNotHaveErrorWhenNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Capsicum annuum");
        }

        [TestCase(TestName = "Update Nutrient Validator should have error for empty description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Description, string.Empty);
        }

        [TestCase(" \n\r\t\r\n\r\n", TestName = "Update Nutrient Validator should have error for whitespace description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Description, name);
        }

        [TestCase(4, TestName = "Update Nutrient Validator should have error for too short description")]
        [TestCase(501, TestName = "Update Nutrient Validator should have error for too long description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsTooShortOrTooLong(int charCount)
        {
            string description = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Description, description);
        }

        [TestCase(null, TestName = "Update Nutrient Validator should not have error for null description")]
        [TestCase("This is an example description of a Nutrient", TestName = "Update Nutrient Validator should not have error for correct description")]
        public void ValidatorShouldNotHaveErrorWhenDescriptionIsSpecified(string value)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, value);
        }

        [TestCase(TestName = "Update Nutrient Validator should have error for empty DosageUnit")]
        public void ValidatorShouldHaveErrorWhenDosageUnitIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.DosageUnits, string.Empty);
        }

        [TestCase(null, TestName = "Update Nutrient Validator should have error for null Dosage Unit")]
        [TestCase(" \n\r\t\r\n\r\n", TestName = "Update Nutrient Validator should have error for whitespace Dosage Unit")]
        public void ValidatorShouldHaveErrorWhenDosageUnitIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.DosageUnits, name);
        }

        [TestCase(21, TestName = "Update Nutrient Validator should have error for too long Dosage Unit")]
        public void ValidatorShouldHaveErrorWhenDosageUnitIsTooShortOrTooLong(int charCount)
        {
            string dosageUnit = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.DosageUnits, dosageUnit);
        }

        [TestCase("milliliters", TestName = "Update Nutrient Validator should not have error for correct Dosage Unit")]
        public void ValidatorShouldNotHaveErrorWhenDosageUnitIsSpecified(string value)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.DosageUnits, value);
        }
    }
}