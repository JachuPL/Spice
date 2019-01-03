using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Plants;
using System;

namespace Spice.Domain.Tests.Plants
{
    [TestFixture]
    internal sealed class AdministeredNutritionTests
    {
        private readonly Plant _nutritionedPlant = new Plant("Test", new Species(), new Field(), 0, 0);
        private readonly Nutrient _nutrient = new Nutrient() { Name = "Water", DosageUnits = "ml" };

        private AdministeredNutrient CreateAdministeredNutrient() =>
            _nutritionedPlant.AdministerNutrient(_nutrient, 1.0, DateTime.Now);

        [TestCase(TestName = "Get and Set administered nutrition Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            AdministeredNutrient administeredNutrient = CreateAdministeredNutrient();
            Guid id = Guid.NewGuid();

            // When
            administeredNutrient.Id = id;

            // Then
            administeredNutrient.Id.Should().Be(id);
        }

        [TestCase(TestName = "Get and Set administered nutrition Plant property works properly")]
        public void GetAndSetPlantWorksProperly()
        {
            // Given
            AdministeredNutrient administeredNutrient = CreateAdministeredNutrient();
            Plant plant = new Plant("Test", new Species(), new Field(), 0, 0);

            // When
            administeredNutrient.Plant = plant;

            // Then
            administeredNutrient.Plant.Should().Be(plant);
        }

        [TestCase(TestName = "Get and Set administered nutrition nutrient property works properly")]
        public void GetAndSetNutrientWorksProperly()
        {
            // Given
            AdministeredNutrient administeredNutrient = CreateAdministeredNutrient();
            Nutrient nutrient = new Nutrient();

            // When
            administeredNutrient.Nutrient = nutrient;

            // Then
            administeredNutrient.Nutrient.Should().Be(nutrient);
        }

        [TestCase(TestName = "Get and Set administered nutrition Amount property works properly")]
        public void GetAndSetAmountWorksProperly()
        {
            // Given
            AdministeredNutrient administeredNutrient = CreateAdministeredNutrient();
            double amount = 0.75;

            // When
            administeredNutrient.Amount = amount;

            // Then
            administeredNutrient.Amount.Should().Be(amount);
        }

        [TestCase(TestName = "Get and Set administered nutrition Date property works properly")]
        public void GetAndSetDateWorksProperly()
        {
            // Given
            AdministeredNutrient administeredNutrient = CreateAdministeredNutrient();
            DateTime date = DateTime.Now.AddDays(1);

            // When
            administeredNutrient.Date = date;

            // Then
            administeredNutrient.Date.Should().Be(date);
        }
    }
}