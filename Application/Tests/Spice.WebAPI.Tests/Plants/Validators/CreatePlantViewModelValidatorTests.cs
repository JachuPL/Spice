﻿using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Plants;
using Spice.ViewModels.Plants.Validators;
using Spice.WebAPI.Tests.Common;

namespace Spice.WebAPI.Tests.Plants.Validators
{
    [TestFixture]
    internal sealed class CreatePlantViewModelValidatorTests
    {
        private CreatePlantViewModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreatePlantViewModelValidator();
        }

        [TestCase(" \n\r\t", TestName = "Create Plant Validator should have error for whitespace name")]
        [TestCase(null, TestName = "Create Plant Validator should have error for null name")]
        public void ValidatorShouldHaveErrorWhenNameIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Create Plant Validator should have error for empty name")]
        public void ValidatorShouldHaveErrorWhenNameIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [TestCase(1, TestName = "Create Plant Validator should have error for too short name")]
        [TestCase(51, TestName = "Create Plant Validator should have error for too long name")]
        public void ValidatorShouldHaveErrorWhenNameIsTooShortOrTooLong(int charCount)
        {
            string name = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [TestCase(TestName = "Create Plant Validator should not have error for correct name")]
        public void ValidatorShouldNotHaveErrorWhenNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Capsicum annuum");
        }

        [TestCase(PlantStateViewModel.Healthy, TestName = "Create Plant Validator should not have error for Healty state")]
        [TestCase(PlantStateViewModel.Deceased, TestName = "Create Plant Validator should not have error for Deceased state")]
        [TestCase(PlantStateViewModel.Flowering, TestName = "Create Plant Validator should not have error for Flowering state")]
        [TestCase(PlantStateViewModel.Fruiting, TestName = "Create Plant Validator should not have error for Fruiting state")]
        [TestCase(PlantStateViewModel.Harvested, TestName = "Create Plant Validator should not have error for Harvested state")]
        [TestCase(PlantStateViewModel.Sick, TestName = "Create Plant Validator should not have error for Sick state")]
        public void ValidatorShouldNotHaveErrorWhenStateIsValid(PlantStateViewModel value)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.State, value);
        }
    }
}