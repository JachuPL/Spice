﻿using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Plants.Validators;
using Spice.WebAPI.Tests.Common;

namespace Spice.WebAPI.Tests.Plants.Validators
{
    [TestFixture]
    internal sealed class UpdatePlantViewModelValidatorTests
    {
        private UpdatePlantViewModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UpdatePlantViewModelValidator();
        }

        [TestCase(" \n\r\t", TestName = "Update Plant Validator should have error for whitespace name")]
        [TestCase(null, TestName = "Update Plant Validator should have error for null name")]
        public void ValidatorShouldHaveErrorWhenNameIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Update Plant Validator should have error for empty name")]
        public void ValidatorShouldHaveErrorWhenNameIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [TestCase(1, TestName = "Update Plant Validator should have error for too short name")]
        [TestCase(51, TestName = "Update Plant Validator should have error for too long name")]
        public void ValidatorShouldHaveErrorWhenNameIsTooShortOrTooLong(int charCount)
        {
            string name = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Update Plant Validator should not have error for correct name")]
        public void ValidatorShouldNotHaveErrorWhenNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Capsicum annuum");
        }

        [TestCase(TestName = "Update Plant Validator should have child validator for State")]
        public void ValidatorShouldHaveChildValidatorForState()
        {
            _validator.ShouldHaveChildValidator(x => x.State, typeof(PlantStateViewModelValidator));
        }
    }
}