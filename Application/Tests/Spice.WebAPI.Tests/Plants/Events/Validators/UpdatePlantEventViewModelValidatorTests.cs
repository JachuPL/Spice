using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Plants.Events;
using Spice.ViewModels.Plants.Events.Validators;
using Spice.WebAPI.Tests.Common;
using System;

namespace Spice.WebAPI.Tests.Plants.Events.Validators
{
    [TestFixture]
    internal sealed class UpdatePlantEventViewModelValidatorTests
    {
        private UpdatePlantEventViewModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UpdatePlantEventViewModelValidator();
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
            _validator.ShouldNotHaveValidationErrorFor(x => x.Type, value);
        }

        [TestCase(-1, TestName = "Update Plant Event Validator should have error for negative numeric value as type")]
        [TestCase(999, TestName = "Update Plant Event Validator should have error for any other numeric value as type")]
        public void ValidatorShouldHaveErrorWhenTypeIsInvalid(EventTypeViewModel value)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Type, value);
        }

        [TestCase(TestName = "Update Plant Event Validator should have error for Occurence date in future")]
        public void ValidatorShouldHaveErrorWhenOccurenceDateIsInFuture()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Occured, DateTime.Now.AddDays(1));
        }

        [TestCase(TestName = "Update Plant Event Validator should not have error for Occurence date before future")]
        public void ValidatorShouldNotHaveErrorWhenOccurenceDateIsNotInFuture()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Occured, DateTime.Now.AddDays(-1));
        }

        [TestCase(TestName = "Update Plant Event Validator should have error for empty description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Description, string.Empty);
        }

        [TestCase(" \n\r\t\r\n\r\n", TestName = "Update Plant Event Validator should have error for whitespace description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsIncorrect(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Description, name);
        }

        [TestCase(1, TestName = "Update Plant Event Validator should have error for too short description")]
        [TestCase(501, TestName = "Update Plant Event Validator should have error for too long description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsTooShortOrTooLong(int charCount)
        {
            string description = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Description, description);
        }

        [TestCase(null, TestName = "Update Plant Event Validator should not have error for null description")]
        [TestCase("This is an example description of a Plant Event", TestName = "Update Plant Event Validator should not have error for correct description")]
        public void ValidatorShouldNotHaveErrorWhenDescriptionIsSpecified(string value)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, value);
        }
    }
}