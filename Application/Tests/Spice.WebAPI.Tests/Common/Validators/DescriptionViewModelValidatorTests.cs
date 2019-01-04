using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Common.Validators;

namespace Spice.WebAPI.Tests.Common.Validators
{
    [TestFixture]
    internal sealed class DescriptionViewModelValidatorTests
    {
        private DescriptionViewModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new DescriptionViewModelValidator();
        }

        [TestCase(TestName = "Description validator should have error when description is whitespace only")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsWhiteSpace()
        {
            TestValidationResult<string, string> validationResult = _validator.TestValidate("\r\n\t \r\n\t ");
            validationResult.ShouldHaveError();
        }

        [TestCase("", TestName = "Description validator should not have error when description is empty")]
        [TestCase("Correct description", TestName = "Description validator should not have error when correct value is set")]
        public void ValidatorShouldNotHaveErrorForValidValue(string value)
        {
            TestValidationResult<string, string> validationResult = _validator.TestValidate(value);
            validationResult.ShouldNotHaveError();
        }
    }
}