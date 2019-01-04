using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Common.Validators;
using Spice.ViewModels.Species.Validators;
using Spice.WebAPI.Tests.Common;

namespace Spice.WebAPI.Tests.Species.Validators
{
    [TestFixture]
    internal sealed class CreateSpeciesViewModelValidatorTests
    {
        private CreateSpeciesViewModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreateSpeciesViewModelValidator();
        }

        [TestCase(" \n\r\t", TestName = "Create Species Validator should have error for whitespace name")]
        [TestCase(null, TestName = "Create Species Validator should have error for null name")]
        public void ValidatorShouldHaveErrorWhenNameIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Create Species Validator should have error for empty name")]
        public void ValidatorShouldHaveErrorWhenNameIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [TestCase(1, TestName = "Create Species Validator should have error for too short name")]
        [TestCase(51, TestName = "Create Species Validator should have error for too long name")]
        public void ValidatorShouldHaveErrorWhenNameIsTooShortOrTooLong(int charCount)
        {
            string name = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Create Species Validator should not have error for correct name")]
        public void ValidatorShouldNotHaveErrorWhenNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Capsicum annuum");
        }

        [TestCase(TestName = "Create Species Validator should have child validator for Description")]
        public void ValidatorShouldHaveChildDescriptionValidator()
        {
            _validator.ShouldHaveChildValidator(x => x.Description, typeof(DescriptionViewModelValidator));
        }

        [TestCase(1, TestName = "Create Species Validator should have error for too short description")]
        [TestCase(501, TestName = "Create Species Validator should have error for too long description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsTooShortOrTooLong(int charCount)
        {
            string description = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Description, description);
        }

        [TestCase(TestName = "Create Species Validator should have error for empty LatinName")]
        public void ValidatorShouldHaveErrorWhenLatinNameIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.LatinName, string.Empty);
        }

        [TestCase(null, TestName = "Create Species Validator should have error for null latin name")]
        [TestCase(" \n\r\t\r\n\r\n", TestName = "Create Species Validator should have error for whitespace latin name")]
        public void ValidatorShouldHaveErrorWhenLatinNameIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.LatinName, name);
        }

        [TestCase(1, TestName = "Create Species Validator should have error for too short latin name")]
        [TestCase(51, TestName = "Create Species Validator should have error for too long latin name")]
        public void ValidatorShouldHaveErrorWhenLatinNameIsTooShortOrTooLong(int charCount)
        {
            string latinName = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.LatinName, latinName);
        }

        [TestCase("milliliters", TestName = "Create Species Validator should not have error for correct latin name")]
        public void ValidatorShouldNotHaveErrorWhenLatinNameIsSpecified(string value)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.LatinName, value);
        }
    }
}