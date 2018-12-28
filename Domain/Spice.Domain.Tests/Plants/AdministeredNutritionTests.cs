using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Plants;
using System;

namespace Spice.Domain.Tests.Plants
{
    [TestFixture]
    internal sealed class AdministeredNutritionTests
    {
        [TestCase(TestName = "Get and Set administered nutrition Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            AdministeredNutrient administeredNutrient = new AdministeredNutrient();
            Guid id = Guid.NewGuid();

            // When
            administeredNutrient.Id = id;

            // Then
            administeredNutrient.Id.Should().Be(id);
        }

        [TestCase(TestName = "Get and Set administered nutrition Plant property works properly")]
        public void GetAndSetNameWorksProperly()
        {
            // Given
            AdministeredNutrient administeredNutrient = new AdministeredNutrient();
            Plant plant = new Plant();

            // When
            administeredNutrient.Plant = plant;

            // Then
            administeredNutrient.Plant.Should().Be(plant);
        }

        [TestCase(TestName = "Get and Set administered nutrition nutrient property works properly")]
        public void GetAndSetSpeciesWorksProperly()
        {
            // Given
            AdministeredNutrient administeredNutrient = new AdministeredNutrient();
            Nutrient nutrient = new Nutrient();

            // When
            administeredNutrient.Nutrient = nutrient;

            // Then
            administeredNutrient.Nutrient.Should().Be(nutrient);
        }

        [TestCase(TestName = "Get and Set administered nutrition Amount property works properly")]
        public void GetAndSetFieldWorksProperly()
        {
            // Given
            AdministeredNutrient administeredNutrient = new AdministeredNutrient();
            double amount = 0.75;

            // When
            administeredNutrient.Amount = amount;

            // Then
            administeredNutrient.Amount.Should().Be(amount);
        }

        [TestCase(TestName = "Get and Set administered nutrition Date property works properly")]
        public void GetAndSetRowWorksProperly()
        {
            // Given
            AdministeredNutrient administeredNutrient = new AdministeredNutrient();
            DateTime date = DateTime.Now.AddDays(1);

            // When
            administeredNutrient.Date = date;

            // Then
            administeredNutrient.Date.Should().Be(date);
        }
    }
}