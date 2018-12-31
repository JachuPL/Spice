using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Nutrients.Validators;

namespace Spice.WebAPI.Tests.Nutrients.Validators
{
    [TestFixture]
    internal sealed class UpdateNutrientViewModelValidatorTests
    {
        private UpdateNutrientViewModelValidator Validator;

        [SetUp]
        public void SetUp()
        {
            Validator = new UpdateNutrientViewModelValidator();
        }

        [TestCase(" \n\r\t", TestName = "Update Nutrient Validator should have error for whitespace name")]
        [TestCase(null, TestName = "Update Nutrient Validator should have error for null name")]
        public void ValidatorShouldHaveErrorWhenNameIsIncorrect(string name)
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Update Nutrient Validator should have error for empty name")]
        public void ValidatorShouldHaveErrorWhenNameIsEmpty()
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [TestCase(1, TestName = "Update Nutrient Validator should have error for too short name")]
        [TestCase(51, TestName = "Update Nutrient Validator should have error for too long name")]
        public void ValidatorShouldHaveErrorWhenNameIsTooShortOrTooLong(int charCount)
        {
            string name = string.Empty;
            for (int i = 0; i < charCount; i++)
                name += "a";

            Validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Update Nutrient Validator should not have error for correct name")]
        public void ValidatorShouldNotHaveErrorWhenNameIsSpecified()
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Capsicum annuum");
        }

        [TestCase(TestName = "Update Nutrient Validator should have error for empty description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsEmpty()
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Description, string.Empty);
        }

        [TestCase(" \n\r\t\r\n\r\n", TestName = "Update Nutrient Validator should have error for whitespace description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsIncorrect(string name)
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Description, name);
        }

        [TestCase(4, TestName = "Update Nutrient Validator should have error for too short description")]
        [TestCase(501, TestName = "Update Nutrient Validator should have error for too long description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsTooShortOrTooLong(int charCount)
        {
            string description = string.Empty;
            for (int i = 0; i < charCount; i++)
                description += "a";

            Validator.ShouldHaveValidationErrorFor(x => x.Description, description);
        }

        [TestCase(null, TestName = "Update Nutrient Validator should not have error for null description")]
        [TestCase("This is an example description of a Nutrient", TestName = "Update Nutrient Validator should not have error for correct description")]
        public void ValidatorShouldNotHaveErrorWhenDescriptionIsSpecified(string value)
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Description, value);
        }

        [TestCase(TestName = "Update Nutrient Validator should have error for empty DosageUnit")]
        public void ValidatorShouldHaveErrorWhenDosageUnitIsEmpty()
        {
            Validator.ShouldHaveValidationErrorFor(x => x.DosageUnits, string.Empty);
        }

        [TestCase(null, TestName = "Update Nutrient Validator should have error for null Dosage Unit")]
        [TestCase(" \n\r\t\r\n\r\n", TestName = "Update Nutrient Validator should have error for whitespace Dosage Unit")]
        public void ValidatorShouldHaveErrorWhenDosageUnitIsIncorrect(string name)
        {
            Validator.ShouldHaveValidationErrorFor(x => x.DosageUnits, name);
        }

        [TestCase(21, TestName = "Update Nutrient Validator should have error for too long Dosage Unit")]
        public void ValidatorShouldHaveErrorWhenDosageUnitIsTooShortOrTooLong(int charCount)
        {
            string dosageUnit = string.Empty;
            for (int i = 0; i < charCount; i++)
                dosageUnit += "a";

            Validator.ShouldHaveValidationErrorFor(x => x.DosageUnits, dosageUnit);
        }

        [TestCase("milliliters", TestName = "Update Nutrient Validator should not have error for correct Dosage Unit")]
        public void ValidatorShouldNotHaveErrorWhenDosageUnitIsSpecified(string value)
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.DosageUnits, value);
        }
    }
}