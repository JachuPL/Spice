using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Common.Validators;
using Spice.ViewModels.Species.Validators;
using Spice.WebAPI.Tests.Common;

namespace Spice.WebAPI.Tests.Species.Validators
{
    [TestFixture]
    internal sealed class UpdateSpeciesViewModelValidatorTests
    {
        private UpdateSpeciesViewModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UpdateSpeciesViewModelValidator();
        }

        [TestCase(" \n\r\t", TestName = "Update Species Validator should have error for whitespace name")]
        [TestCase(null, TestName = "Update Species Validator should have error for null name")]
        public void ValidatorShouldHaveErrorWhenNameIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Update Species Validator should have error for empty name")]
        public void ValidatorShouldHaveErrorWhenNameIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [TestCase(1, TestName = "Update Species Validator should have error for too short name")]
        [TestCase(51, TestName = "Update Species Validator should have error for too long name")]
        public void ValidatorShouldHaveErrorWhenNameIsTooShortOrTooLong(int charCount)
        {
            string name = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Update Species Validator should not have error for correct name")]
        public void ValidatorShouldNotHaveErrorWhenNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Capsicum annuum");
        }

        [TestCase(TestName = "Update Species Validator should have error for empty description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Description, string.Empty);
        }

        [TestCase(TestName = "Update Species Validator should have child validator for Description")]
        public void ValidatorShouldHaveChildDescriptionValidator()
        {
            _validator.ShouldHaveChildValidator(x => x.Description, typeof(DescriptionViewModelValidator));
        }

        [TestCase(1, TestName = "Update Species Validator should have error for too short description")]
        [TestCase(501, TestName = "Update Species Validator should have error for too long description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsTooShortOrTooLong(int charCount)
        {
            string description = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Description, description);
        }

        [TestCase(TestName = "Update Species Validator should have error for empty LatinName")]
        public void ValidatorShouldHaveErrorWhenLatinNameIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.LatinName, string.Empty);
        }

        [TestCase(null, TestName = "Update Species Validator should have error for null latin name")]
        [TestCase(" \n\r\t\r\n\r\n", TestName = "Update Species Validator should have error for whitespace latin name")]
        public void ValidatorShouldHaveErrorWhenLatinNameIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.LatinName, name);
        }

        [TestCase(1, TestName = "Update Species Validator should have error for too short latin name")]
        [TestCase(51, TestName = "Update Species Validator should have error for too long latin name")]
        public void ValidatorShouldHaveErrorWhenLatinNameIsTooShortOrTooLong(int charCount)
        {
            string latinName = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.LatinName, latinName);
        }

        [TestCase("milliliters", TestName = "Update Species Validator should not have error for correct latin name")]
        public void ValidatorShouldNotHaveErrorWhenLatinNameIsSpecified(string value)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.LatinName, value);
        }
    }
}