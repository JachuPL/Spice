using FluentValidation.TestHelper;
using NUnit.Framework;
using Spice.ViewModels.Common.Validators;
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

        [TestCase(TestName = "Create Plant Event Validator should have child validator for event type")]
        public void ValidatorShouldHaveChildValidatorForEventType()
        {
            _validator.ShouldHaveChildValidator(x => x.Type, typeof(EventTypeViewModelValidator));
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

        [TestCase(TestName = "Create Plant Event Validator should have child validator for Description")]
        public void ValidatorShouldHaveChildDescriptionValidator()
        {
            _validator.ShouldHaveChildValidator(x => x.Description, typeof(DescriptionViewModelValidator));
        }

        [TestCase(1, TestName = "Create Plant Event Validator should have error for too short description")]
        [TestCase(501, TestName = "Create Plant Event Validator should have error for too long description")]
        public void ValidatorShouldHaveErrorWhenDescriptionIsTooShortOrTooLong(int charCount)
        {
            string description = StringGenerator.Generate(charCount);
            _validator.ShouldHaveValidationErrorFor(x => x.Description, description);
        }
    }
}