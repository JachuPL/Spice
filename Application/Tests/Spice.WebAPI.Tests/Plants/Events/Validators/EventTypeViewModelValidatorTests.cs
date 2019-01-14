using FluentAssertions;
using FluentValidation.Results;
using NUnit.Framework;
using Spice.ViewModels.Plants.Events;
using Spice.ViewModels.Plants.Events.Validators;

namespace Spice.WebAPI.Tests.Plants.Events.Validators
{
    internal sealed class EventTypeViewModelValidatorTests
    {
        private EventTypeViewModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new EventTypeViewModelValidator();
        }

        [TestCase(EventTypeViewModel.OverWatering, TestName = "Event Type Validator should not have error for Overwatering type")]
        [TestCase(EventTypeViewModel.Disease, TestName = "Event Type Validator should not have error for Disease type")]
        [TestCase(EventTypeViewModel.Fungi, TestName = "Event Type Validator should not have error for Fungi type")]
        [TestCase(EventTypeViewModel.Growth, TestName = "Event Type Validator should not have error for Growth type")]
        [TestCase(EventTypeViewModel.Insects, TestName = "Event Type Validator should not have error for Insects type")]
        [TestCase(EventTypeViewModel.Pests, TestName = "Event Type Validator should not have error for Pests type")]
        [TestCase(EventTypeViewModel.UnderWatering, TestName = "Event Type Validator should not have error for Underwatering type")]
        [TestCase(EventTypeViewModel.Nutrition, TestName = "Event Type Validator should not have error for Nutrition type")]
        [TestCase(EventTypeViewModel.Sprouting, TestName = "Event Type Validator should not have error for Sprouting type")]
        public void ValidatorShouldNotHaveErrorWhenTypeIsValid(EventTypeViewModel value)
        {
            ValidationResult result = _validator.Validate(value);
            result.Errors.Should().BeEmpty();
        }

        [TestCase(-1, TestName = "Event Type Validator should have error for negative numeric value as type")]
        [TestCase(999, TestName = "Event Type Validator should have error for any other numeric value as type")]
        [TestCase(EventTypeViewModel.Moving, TestName = "Event Type Validator should have error for Moving type")]
        [TestCase(EventTypeViewModel.Start, TestName = "Event Type Validator should have error for Start type")]
        public void ValidatorShouldHaveErrorWhenTypeIsInvalid(EventTypeViewModel value)
        {
            ValidationResult result = _validator.Validate(value);
            result.Errors.Should().NotBeEmpty();
        }
    }
}