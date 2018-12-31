using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Species.Validators;

namespace Spice.WebAPI.Tests.Species.Validators
{
    [TestFixture]
    internal sealed class CreateSpeciesViewModelValidatorTests
    {
        private CreateSpeciesViewModelValidator Validator;

        [SetUp]
        public void SetUp()
        {
            Validator = new CreateSpeciesViewModelValidator();
        }

        [TestCase(" \n\r\t", TestName = "Create Species Validator should have error for whitespace name")]
        [TestCase(null, TestName = "Create Species Validator should have error for null name")]
        public void ValidatorShouldHaveErrorWhenNameIsIncorrect(string name)
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Create Species Validator should have error for empty name")]
        public void ValidatorShouldHaveErrorWhenNameIsEmpty()
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [TestCase(1, TestName = "Create Species Validator should have error for too short name")]
        [TestCase(51, TestName = "Create Species Validator should have error for too long name")]
        public void ValidatorShouldHaveErrorWhenNameIsTooShortOrTooLong(int charCount)
        {
            string name = string.Empty;
            for (int i = 0; i < charCount; i++)
                name += "a";

            Validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Create Species Validator should not have error for correct name")]
        public void ValidatorShouldNotHaveErrorWhenNameIsSpecified()
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Capsicum annuum");
        }

        [TestCase(TestName = "Create Species Validator should have error for empty description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsEmpty()
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Description, string.Empty);
        }

        [TestCase(" \n\r\t\r\n\r\n", TestName = "Create Species Validator should not have error for whitespace description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsIncorrect(string name)
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Description, name);
        }

        [TestCase(1, TestName = "Create Species Validator should have error for too short description")]
        [TestCase(501, TestName = "Create Species Validator should have error for too long description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsTooShortOrTooLong(int charCount)
        {
            string description = string.Empty;
            for (int i = 0; i < charCount; i++)
                description += "a";

            Validator.ShouldHaveValidationErrorFor(x => x.Description, description);
        }

        [TestCase(null, TestName = "Create Species Validator should not have error for null description")]
        [TestCase("This is an example description of a Nutrient", TestName = "Create Species Validator should not have error for correct description")]
        public void ValidatorShouldNotHaveErrorWhenDescriptionIsSpecified(string value)
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Description, value);
        }

        [TestCase(TestName = "Create Species Validator should have error for empty LatinName")]
        public void ValidatorShouldHaveErrorWhenLatinNameIsEmpty()
        {
            Validator.ShouldHaveValidationErrorFor(x => x.LatinName, string.Empty);
        }

        [TestCase(null, TestName = "Create Species Validator should have error for null latin name")]
        [TestCase(" \n\r\t\r\n\r\n", TestName = "Create Species Validator should have error for whitespace latin name")]
        public void ValidatorShouldHaveErrorWhenLatinNameIsIncorrect(string name)
        {
            Validator.ShouldHaveValidationErrorFor(x => x.LatinName, name);
        }

        [TestCase(1, TestName = "Create Species Validator should have error for too short latin name")]
        [TestCase(51, TestName = "Create Species Validator should have error for too long latin name")]
        public void ValidatorShouldHaveErrorWhenLatinNameIsTooShortOrTooLong(int charCount)
        {
            string latinName = string.Empty;
            for (int i = 0; i < charCount; i++)
                latinName += "a";

            Validator.ShouldHaveValidationErrorFor(x => x.LatinName, latinName);
        }

        [TestCase("milliliters", TestName = "Create Species Validator should not have error for correct latin name")]
        public void ValidatorShouldNotHaveErrorWhenLatinNameIsSpecified(string value)
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.LatinName, value);
        }
    }
}