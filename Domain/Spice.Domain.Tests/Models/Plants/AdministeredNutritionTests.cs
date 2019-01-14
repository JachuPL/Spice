using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Builders;
using Spice.Domain.Plants;
using System;

namespace Spice.Domain.Tests.Models.Plants
{
    [TestFixture]
    internal sealed class AdministeredNutritionTests : AbstractBaseDomainTestFixture<AdministeredNutrient>
    {
        private readonly Plant _nutritionedPlant =
            New.Plant.WithName("Test").WithSpecies(New.Species.WithName("Pepper").WithLatinName("Capsicum Annuum"))
               .WithField(New.Field.WithName("Test field"))
               .InRow(0)
               .InColumn(0);

        private readonly Nutrient _nutrient = New.Nutrient.WithName("Water").WithDosageUnits("ml");

        protected override AdministeredNutrient CreateDomainObject() =>
            _nutritionedPlant.AdministerNutrient(_nutrient, 1.0, DateTime.Now);

        [TestCase(TestName = "Get and Set administered nutrition Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            Guid id = Guid.NewGuid();

            // When
            DomainObject.Id = id;

            // Then
            DomainObject.Id.Should().Be(id);
        }

        [TestCase(TestName = "Get and Set administered nutrition Plant property works properly")]
        public void GetAndSetPlantWorksProperly()
        {
            // Given
            Plant plant = New.Plant.WithName("Test").WithSpecies(New.Species.WithName("Pepper")
                                                                            .WithLatinName("Capsicum Annuum"))
                             .WithField(New.Field.WithName("Test field"))
                             .InRow(0)
                             .InColumn(0);

            // When
            DomainObject.Plant = plant;

            // Then
            DomainObject.Plant.Should().Be(plant);
        }

        [TestCase(TestName = "Get and Set administered nutrition nutrient property works properly")]
        public void GetAndSetNutrientWorksProperly()
        {
            // Given
            Nutrient nutrient = New.Nutrient.WithName("Test").WithDescription("Test desc").WithDosageUnits("g");

            // When
            DomainObject.Nutrient = nutrient;

            // Then
            DomainObject.Nutrient.Should().Be(nutrient);
        }

        [TestCase(TestName = "Get and Set administered nutrition Amount property works properly")]
        public void GetAndSetAmountWorksProperly()
        {
            // Given
            double amount = 0.75;

            // When
            DomainObject.Amount = amount;

            // Then
            DomainObject.Amount.Should().Be(amount);
        }

        [TestCase(TestName = "Get and Set administered nutrition Date property works properly")]
        public void GetAndSetDateWorksProperly()
        {
            // Given
            DateTime date = DateTime.Now.AddDays(1);

            // When
            DomainObject.Date = date;

            // Then
            DomainObject.Date.Should().Be(date);
        }
    }
}