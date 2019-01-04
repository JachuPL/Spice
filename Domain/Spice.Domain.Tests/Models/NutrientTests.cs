using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Plants;
using System;
using System.Collections.Generic;

namespace Spice.Domain.Tests.Models
{
    [TestFixture]
    internal sealed class NutrientTests : AbstractBaseDomainTestFixture<Nutrient>
    {
        protected override Nutrient CreateDomainObject() => new Nutrient
        {
            Name = "Test",
            Description = "Test desc",
            DosageUnits = "g"
        };

        [TestCase(TestName = "Get and Set nutrient Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            Guid id = Guid.NewGuid();

            // When
            DomainObject.Id = id;

            // Then
            DomainObject.Id.Should().Be(id);
        }

        [TestCase(TestName = "Get and Set nutrient Name property works properly")]
        public void GetAndSetNameWorksProperly()
        {
            // Given
            string name = Guid.Empty.ToString();

            // When
            DomainObject.Name = name;

            // Then
            DomainObject.Name.Should().Be(name);
        }

        [TestCase(TestName = "Get and Set nutrient Description property works properly")]
        public void GetAndSetDescriptionWorksProperly()
        {
            // Given
            string description = Guid.Empty.ToString();

            // When
            DomainObject.Description = description;

            // Then
            DomainObject.Description.Should().Be(description);
        }

        [TestCase(TestName = "Get and Set nutrient Dosage Units property works properly")]
        public void GetAndSetDosageUnitsWorksProperly()
        {
            // Given
            string dosageUnits = Guid.Empty.ToString();

            // When
            DomainObject.DosageUnits = dosageUnits;

            // Then
            DomainObject.DosageUnits.Should().Be(dosageUnits);
        }

        [TestCase(TestName = "Get and Set nutritioned plants collection property works properly")]
        public void GetAndSetAdministeredToPlantsWorksProperly()
        {
            // Given
            ICollection<AdministeredNutrient> administeredNutrients = new List<AdministeredNutrient>();

            // When
            DomainObject.AdministeredToPlants = administeredNutrients;

            // Then
            DomainObject.AdministeredToPlants.Should().BeSameAs(administeredNutrients);
        }
    }
}