using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Plants.Events;
using Spice.ViewModels.Plants.Events.Validators;
using System;

namespace Spice.WebAPI.Tests.Plants.Events.Validators
{
    [TestFixture]
    internal sealed class UpdatePlantEventViewModelValidatorTests
    {
        private UpdatePlantEventViewModelValidator Validator;

        [SetUp]
        public void SetUp()
        {
            Validator = new UpdatePlantEventViewModelValidator();
        }

        [TestCase(EventTypeViewModel.OverWatering, TestName = "Update Plant Event Validator should not have error for Overwatering type")]
        [TestCase(EventTypeViewModel.Disease, TestName = "Update Plant Event Validator should not have error for Disease type")]
        [TestCase(EventTypeViewModel.Fungi, TestName = "Update Plant Event Validator should not have error for Fungi type")]
        [TestCase(EventTypeViewModel.Growth, TestName = "Update Plant Event Validator should not have error for Growth type")]
        [TestCase(EventTypeViewModel.Insects, TestName = "Update Plant Event Validator should not have error for Insects type")]
        [TestCase(EventTypeViewModel.Pests, TestName = "Update Plant Event Validator should not have error for Pests type")]
        [TestCase(EventTypeViewModel.UnderWatering, TestName = "Update Plant Event Validator should not have error for Underwatering type")]
        public void ValidatorShouldNotHaveErrorWhenTypeIsValid(EventTypeViewModel value)
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Type, value);
        }

        [TestCase(-1, TestName = "Update Plant Event Validator should have error for negative numeric value as type")]
        [TestCase(999, TestName = "Update Plant Event Validator should have error for any other numeric value as type")]
        public void ValidatorShouldHaveErrorWhenTypeIsInvalid(EventTypeViewModel value)
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Type, value);
        }

        [TestCase(TestName = "Update Plant Event Validator should have error for Occurence date in future")]
        public void ValidatorShouldHaveErrorWhenOccurenceDateIsInFuture()
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Occured, DateTime.Now.AddDays(1));
        }

        [TestCase(TestName = "Update Plant Event Validator should not have error for Occurence date before future")]
        public void ValidatorShouldNotHaveErrorWhenOccurenceDateIsNotInFuture()
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Occured, DateTime.Now.AddDays(-1));
        }

        [TestCase(TestName = "Update Plant Event Validator should have error for empty description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsEmpty()
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Description, string.Empty);
        }

        [TestCase(" \n\r\t\r\n\r\n", TestName = "Update Plant Event Validator should have error for whitespace description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsIncorrect(string name)
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Description, name);
        }

        [TestCase(1, TestName = "Update Plant Event Validator should have error for too short description")]
        [TestCase(501, TestName = "Update Plant Event Validator should have error for too long description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsTooShortOrTooLong(int charCount)
        {
            string description = string.Empty;
            for (int i = 0; i < charCount; i++)
                description += "a";

            Validator.ShouldHaveValidationErrorFor(x => x.Description, description);
        }

        [TestCase(null, TestName = "Update Plant Event Validator should not have error for null description")]
        [TestCase("This is an example description of a Plant Event", TestName = "Update Plant Event Validator should not have error for correct description")]
        public void ValidatorShouldNotHaveErrorWhenDescriptionIsSpecified(string value)
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Description, value);
        }
    }
}