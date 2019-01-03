using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Plants.Events;
using Spice.ViewModels.Plants.Events.Validators;
using Spice.WebAPI.Tests.Common;
using System;

namespace Spice.WebAPI.Tests.Plants.Events.Validators
{
    [TestFixture]
    internal sealed class CreatePlantEventViewModelValidatorTests
    {
        private CreatePlantEventViewModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreatePlantEventViewModelValidator();
        }

        [TestCase(EventTypeViewModel.OverWatering, TestName = "Create Plant Event Validator should not have error for Overwatering type")]
        [TestCase(EventTypeViewModel.Disease, TestName = "Create Plant Event Validator should not have error for Disease type")]
        [TestCase(EventTypeViewModel.Fungi, TestName = "Create Plant Event Validator should not have error for Fungi type")]
        [TestCase(EventTypeViewModel.Growth, TestName = "Create Plant Event Validator should not have error for Growth type")]
        [TestCase(EventTypeViewModel.Insects, TestName = "Create Plant Event Validator should not have error for Insects type")]
        [TestCase(EventTypeViewModel.Pests, TestName = "Create Plant Event Validator should not have error for Pests type")]
        [TestCase(EventTypeViewModel.UnderWatering, TestName = "Create Plant Event Validator should not have error for Underwatering type")]
        [TestCase(EventTypeViewModel.Nutrition, TestName = "Create Plant Event Validator should not have error for Nutrition type")]
        public void ValidatorShouldNotHaveErrorWhenTypeIsValid(EventTypeViewModel value)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Type, value);
        }

        [TestCase(-1, TestName = "Create Plant Event Validator should have error for negative numeric value as type")]
        [TestCase(999, TestName = "Create Plant Event Validator should have error for any other numeric value as type")]
        [TestCase(EventTypeViewModel.Moving, TestName = "Create Plant Event Validator should have error for Moving type")]
        [TestCase(EventTypeViewModel.Start, TestName = "Create Plant Event Validator should have error for Start type")]
        public void ValidatorShouldHaveErrorWhenTypeIsInvalid(EventTypeViewModel value)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Type, value);
        }

        [TestCase(TestName = "Create Plant Event Validator should have error for Occurence date in future")]
        public void ValidatorShouldHaveErrorWhenOccurenceDateIsInFuture()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Occured, DateTime.Now.AddDays(1));
        }

        [TestCase(TestName = "Create Plant Event Validator should not have error for Occurence date before future")]
        public void ValidatorShouldNotHaveErrorWhenOccurenceDateIsNotInFuture()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Occured, DateTime.Now.AddDays(-1));
        }

        [TestCase(TestName = "Create Plant Event Validator should have error for empty description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Description, string.Empty);
        }

        [TestCase(" \n\r\t\r\n\r\n", TestName = "Create Plant Event Validator should have error for whitespace description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Description, name);
        }

        [TestCase(1, TestName = "Create Plant Event Validator should have error for too short description")]
        [TestCase(501, TestName = "Create Plant Event Validator should have error for too long description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsTooShortOrTooLong(int charCount)
        {
            string description = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Description, description);
        }

        [TestCase(null, TestName = "Create Plant Event Validator should not have error for null description")]
        [TestCase("This is an example description of a Plant Event", TestName = "Create Plant Event Validator should not have error for correct description")]
        public void ValidatorShouldNotHaveErrorWhenDescriptionIsSpecified(string value)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, value);
        }
    }
}