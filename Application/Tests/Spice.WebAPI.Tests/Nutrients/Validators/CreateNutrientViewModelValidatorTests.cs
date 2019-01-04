using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Common.Validators;
using Spice.ViewModels.Nutrients.Validators;
using Spice.WebAPI.Tests.Common;

namespace Spice.WebAPI.Tests.Nutrients.Validators
{
    [TestFixture]
    internal sealed class CreateNutrientViewModelValidatorTests
    {
        private CreateNutrientViewModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreateNutrientViewModelValidator();
        }

        [TestCase(" \n\r\t", TestName = "Create Nutrient Validator should have error for whitespace name")]
        [TestCase(null, TestName = "Create Nutrient Validator should have error for null name")]
        public void ValidatorShouldHaveErrorWhenNameIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Create Nutrient Validator should have error for empty name")]
        public void ValidatorShouldHaveErrorWhenNameIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [TestCase(1, TestName = "Create Nutrient Validator should have error for too short name")]
        [TestCase(51, TestName = "Create Nutrient Validator should have error for too long name")]
        public void ValidatorShouldHaveErrorWhenNameIsTooShortOrTooLong(int charCount)
        {
            string name = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Create Nutrient Validator should not have error for correct name")]
        public void ValidatorShouldNotHaveErrorWhenNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Capsicum annuum");
        }

        [TestCase(TestName = "Create Nutrient Validator should have child validator for Description")]
        public void ValidatorShouldHaveChildDescriptionValidator()
        {
            _validator.ShouldHaveChildValidator(x => x.Description, typeof(DescriptionViewModelValidator));
        }

        [TestCase(4, TestName = "Create Nutrient Validator should have error for too short description")]
        [TestCase(501, TestName = "Create Nutrient Validator should have error for too long description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsTooShortOrTooLong(int charCount)
        {
            string description = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Description, description);
        }

        [TestCase(TestName = "Create Nutrient Validator should have error for empty DosageUnit")]
        public void ValidatorShouldHaveErrorWhenDosageUnitIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.DosageUnits, string.Empty);
        }

        [TestCase(null, TestName = "Create Nutrient Validator should have error for null Dosage Unit")]
        [TestCase(" \n\r\t\r\n\r\n", TestName = "Create Nutrient Validator should have error for whitespace Dosage Unit")]
        public void ValidatorShouldHaveErrorWhenDosageUnitIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.DosageUnits, name);
        }

        [TestCase(21, TestName = "Create Nutrient Validator should have error for too long Dosage Unit")]
        public void ValidatorShouldHaveErrorWhenDosageUnitIsTooShortOrTooLong(int charCount)
        {
            string dosageUnit = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.DosageUnits, dosageUnit);
        }

        [TestCase("milliliters", TestName = "Create Nutrient Validator should not have error for correct Dosage Unit")]
        public void ValidatorShouldNotHaveErrorWhenDosageUnitIsSpecified(string value)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.DosageUnits, value);
        }
    }
}