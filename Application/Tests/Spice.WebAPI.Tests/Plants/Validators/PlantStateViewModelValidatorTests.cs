using FluentAssertions;
using FluentValidation.Results;
using NUnit.Framework;

namespace Spice.ViewModels.Plants.Validators
{
    [TestFixture]
    public class PlantStateViewModelValidatorTests
    {
        private PlantStateViewModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new PlantStateViewModelValidator();
        }

        [TestCase(PlantStateViewModel.Healthy, TestName = "Update Plant Validator should not have error for Healty state")]
        [TestCase(PlantStateViewModel.Deceased, TestName = "Update Plant Validator should not have error for Deceased state")]
        [TestCase(PlantStateViewModel.Flowering, TestName = "Update Plant Validator should not have error for Flowering state")]
        [TestCase(PlantStateViewModel.Fruiting, TestName = "Update Plant Validator should not have error for Fruiting state")]
        [TestCase(PlantStateViewModel.Harvested, TestName = "Update Plant Validator should not have error for Harvested state")]
        [TestCase(PlantStateViewModel.Sick, TestName = "Update Plant Validator should not have error for Sick state")]
        public void ValidatorShouldNotHaveErrorWhenStateIsValid(PlantStateViewModel value)
        {
            ValidationResult result = _validator.Validate(value);
            result.Errors.Should().BeEmpty();
        }
    }
}