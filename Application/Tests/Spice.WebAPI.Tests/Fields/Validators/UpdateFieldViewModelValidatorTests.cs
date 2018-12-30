using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Fields.Validators;

namespace Spice.WebAPI.Tests.Nutrients.Validators
{
    [TestFixture]
    internal sealed class UpdateFieldViewModelValidatorTests
    {
        private UpdateFieldViewModelValidator Validator;

        [SetUp]
        public void SetUp()
        {
            Validator = new UpdateFieldViewModelValidator();
        }

        [TestCase(" \n\r\t", TestName = "Validator should have error for whitespace name")]
        [TestCase(null, TestName = "Validator should have error for null name")]
        public void ValidatorShouldHaveErrorWhenNameIsIncorrect(string name)
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Validator should have error for empty name")]
        public void ValidatorShouldHaveErrorWhenNameIsEmpty()
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [TestCase(1, TestName = "Validator should have error for too short name")]
        [TestCase(51, TestName = "Validator should have error for too long name")]
        public void ValidatorShouldHaveErrorWhenNameIsTooShortOrTooLong(int charCount)
        {
            string name = string.Empty;
            for (int i = 0; i < charCount; i++)
                name += "a";

            Validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Validator should not have error for correct name")]
        public void ValidatorShouldNotHaveErrorWhenNameIsSpecified()
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Capsicum annuum");
        }

        [TestCase(TestName = "Validator should have error for empty description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsEmpty()
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Description, string.Empty);
        }

        [TestCase(" \n\r\t\r\n\r\n", TestName = "Validator should have error for whitespace description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsIncorrect(string name)
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Description, name);
        }

        [TestCase(4, TestName = "Validator should have error for too short description")]
        [TestCase(501, TestName = "Validator should have error for too long description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsTooShortOrTooLong(int charCount)
        {
            string description = string.Empty;
            for (int i = 0; i < charCount; i++)
                description += "a";

            Validator.ShouldHaveValidationErrorFor(x => x.Description, description);
        }

        [TestCase(null, TestName = "Validator should not have error for null description")]
        [TestCase("This is an example description of a field", TestName = "Validator should not have error for correct description")]
        public void ValidatorShouldNotHaveErrorWhenDescriptionIsSpecified(string value)
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Description, value);
        }

        [TestCase(-91, TestName = "Validator should have error for latitude lower than -90")]
        [TestCase(91, TestName = "Validator should have error for latitude greater than 90")]
        public void ValidatorShouldHaveErrorWhenLatitudeIsInvalid(double value)
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Latitude, value);
        }

        [TestCase(TestName = "Validator should not have error for correct latitude")]
        public void ValidatorShouldNotHaveErrorForValidLatitude()
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Latitude, 90);
        }

        [TestCase(-181, TestName = "Validator should have error for longtitude lower than -180")]
        [TestCase(181, TestName = "Validator should have error for longtitude greater than 180")]
        public void ValidatorShouldHaveErrorWhenLongtitudeIsInvalid(double value)
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Longtitude, value);
        }

        [TestCase(TestName = "Validator should not have error for correct latitude")]
        public void ValidatorShouldNotHaveErrorForValidLongtitude()
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Longtitude, 90);
        }
    }
}