using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Plants.Events;
using Spice.ViewModels.Plants.Events.Validators;
using System;

namespace Spice.WebAPI.Tests.Plants.Events.Validators
{
    [TestFixture]
    internal sealed class CreatePlantEventViewModelValidatorTests
    {
        private CreatePlantEventViewModelValidator Validator;

        [SetUp]
        public void SetUp()
        {
            Validator = new CreatePlantEventViewModelValidator();
        }

        [TestCase(EventTypeViewModel.OverWatering, TestName = "Create Plant Event Validator should not have error for Overwatering type")]
        [TestCase(EventTypeViewModel.Disease, TestName = "Create Plant Event Validator should not have error for Disease type")]
        [TestCase(EventTypeViewModel.Fungi, TestName = "Create Plant Event Validator should not have error for Fungi type")]
        [TestCase(EventTypeViewModel.Growth, TestName = "Create Plant Event Validator should not have error for Growth type")]
        [TestCase(EventTypeViewModel.Insects, TestName = "Create Plant Event Validator should not have error for Insects type")]
        [TestCase(EventTypeViewModel.Moving, TestName = "Create Plant Event Validator should have not error for Moving type")]
        [TestCase(EventTypeViewModel.Pests, TestName = "Create Plant Event Validator should have not error for Pests type")]
        [TestCase(EventTypeViewModel.UnderWatering, TestName = "Create Plant Event Validator should have not error for Underwatering type")]
        public void ValidatorShouldNotHaveErrorWhenTypeIsValid(EventTypeViewModel value)
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Type, value);
        }

        [TestCase(TestName = "Create Plant Event Validator should have error for Occurence date in future")]
        public void ValidatorShouldHaveErrorWhenOccurenceDateIsInFuture()
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Occured, DateTime.Now.AddDays(1));
        }

        [TestCase(TestName = "Create Plant Event Validator should not have error for Occurence date before future")]
        public void ValidatorShouldNotHaveErrorWhenOccurenceDateIsNotInFuture()
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Occured, DateTime.Now.AddDays(-1));
        }

        [TestCase(TestName = "Create Plant Event Validator should have error for empty description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsEmpty()
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Description, string.Empty);
        }

        [TestCase(" \n\r\t\r\n\r\n", TestName = "Create Plant Event Validator should have error for whitespace description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsIncorrect(string name)
        {
            Validator.ShouldHaveValidationErrorFor(x => x.Description, name);
        }

        [TestCase(1, TestName = "Create Plant Event Validator should have error for too short description")]
        [TestCase(501, TestName = "Create Plant Event Validator should have error for too long description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsTooShortOrTooLong(int charCount)
        {
            string description = string.Empty;
            for (int i = 0; i < charCount; i++)
                description += "a";

            Validator.ShouldHaveValidationErrorFor(x => x.Description, description);
        }

        [TestCase(null, TestName = "Create Plant Event Validator should not have error for null description")]
        [TestCase("This is an example description of a Plant Event", TestName = "Create Plant Event Validator should not have error for correct description")]
        public void ValidatorShouldNotHaveErrorWhenDescriptionIsSpecified(string value)
        {
            Validator.ShouldNotHaveValidationErrorFor(x => x.Description, value);
        }
    }
}