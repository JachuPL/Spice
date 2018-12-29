using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Plants;
using System;
using System.Collections.Generic;

namespace Spice.Domain.Tests
{
    [TestFixture]
    internal sealed class NutrientTests
    {
        [TestCase(TestName = "Get and Set nutrient Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            Nutrient nutrient = new Nutrient();
            Guid id = Guid.NewGuid();

            // When
            nutrient.Id = id;

            // Then
            nutrient.Id.Should().Be(id);
        }

        [TestCase(TestName = "Get and Set nutrient Name property works properly")]
        public void GetAndSetNameWorksProperly()
        {
            // Given
            Nutrient nutrient = new Nutrient();
            string name = Guid.Empty.ToString();

            // When
            nutrient.Name = name;

            // Then
            nutrient.Name.Should().Be(name);
        }

        [TestCase(TestName = "Get and Set nutrient Description property works properly")]
        public void GetAndSetDescriptionWorksProperly()
        {
            // Given
            Nutrient nutrient = new Nutrient();
            string description = Guid.Empty.ToString();

            // When
            nutrient.Description = description;

            // Then
            nutrient.Description.Should().Be(description);
        }

        [TestCase(TestName = "Get and Set nutrient Dosage Units property works properly")]
        public void GetAndSetDosageUnitsWorksProperly()
        {
            // Given
            Nutrient nutrient = new Nutrient();
            string dosageUnits = Guid.Empty.ToString();

            // When
            nutrient.DosageUnits = dosageUnits;

            // Then
            nutrient.DosageUnits.Should().Be(dosageUnits);
        }

        [TestCase(TestName = "Get and Set nutritioned plants collection property works properly")]
        public void GetAndSetAdministeredToPlantsWorksProperly()
        {
            // Given
            Nutrient nutrient = new Nutrient();
            ICollection<AdministeredNutrient> administeredNutrients = new List<AdministeredNutrient>();

            // When
            nutrient.AdministeredToPlants = administeredNutrients;

            // Then
            nutrient.AdministeredToPlants.Should().BeSameAs(administeredNutrients);
        }
    }
}