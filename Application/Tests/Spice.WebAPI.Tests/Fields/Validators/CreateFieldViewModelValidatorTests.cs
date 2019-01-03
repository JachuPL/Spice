using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Fields.Validators;
using Spice.WebAPI.Tests.Common;

namespace Spice.WebAPI.Tests.Fields.Validators
{
    [TestFixture]
    internal sealed class CreateFieldViewModelValidatorTests
    {
        private CreateFieldViewModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreateFieldViewModelValidator();
        }

        [TestCase(" \n\r\t", TestName = "Create Field Validator should have error for whitespace name")]
        [TestCase(null, TestName = "Create Field Validator should have error for null name")]
        public void ValidatorShouldHaveErrorWhenNameIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Create Field Validator should have error for empty name")]
        public void ValidatorShouldHaveErrorWhenNameIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [TestCase(1, TestName = "Create Field Validator should have error for too short name")]
        [TestCase(51, TestName = "Create Field Validator should have error for too long name")]
        public void ValidatorShouldHaveErrorWhenNameIsTooShortOrTooLong(int charCount)
        {
            string name = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Create Field Validator should not have error for correct name")]
        public void ValidatorShouldNotHaveErrorWhenNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Capsicum annuum");
        }

        [TestCase(TestName = "Create Field Validator should have error for empty description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Description, string.Empty);
        }

        [TestCase(" \n\r\t\r\n\r\n", TestName = "Create Field Validator should have error for whitespace description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Description, name);
        }

        [TestCase(4, TestName = "Create Field Validator should have error for too short description")]
        [TestCase(501, TestName = "Create Field Validator should have error for too long description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsTooShortOrTooLong(int charCount)
        {
            string description = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Description, description);
        }

        [TestCase(null, TestName = "Create Field Validator should not have error for null description")]
        [TestCase("This is an example description of a field", TestName = "Create Field Validator should not have error for correct description")]
        public void ValidatorShouldNotHaveErrorWhenDescriptionIsSpecified(string value)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, value);
        }

        [TestCase(-91, TestName = "Create Field Validator should have error for latitude lower than -90")]
        [TestCase(91, TestName = "Create Field Validator should have error for latitude greater than 90")]
        public void ValidatorShouldHaveErrorWhenLatitudeIsInvalid(double value)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Latitude, value);
        }

        [TestCase(TestName = "Create Field Validator should not have error for correct latitude")]
        public void ValidatorShouldNotHaveErrorForValidLatitude()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Latitude, 90);
        }

        [TestCase(-181, TestName = "Create Field Validator should have error for longtitude lower than -180")]
        [TestCase(181, TestName = "Create Field Validator should have error for longtitude greater than 180")]
        public void ValidatorShouldHaveErrorWhenLongtitudeIsInvalid(double value)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Longtitude, value);
        }

        [TestCase(TestName = "Create Field Validator should not have error for correct longtitude")]
        public void ValidatorShouldNotHaveErrorForValidLongtitude()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Longtitude, 90);
        }
    }
}