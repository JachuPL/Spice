using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Builders;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using System;
using System.Collections.Generic;

namespace Spice.Domain.Tests.Models.Plants
{
    [TestFixture]
    internal sealed class PlantsTests : AbstractBaseDomainTestFixture<Plant>
    {
        protected override Plant CreateDomainObject() => new Plant("Test", new Species(), New.Field.WithName("Test field"), 0, 0);

        [TestCase(TestName = "Get and Set plant Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            Guid id = Guid.NewGuid();

            // When
            DomainObject.Id = id;

            // Then
            DomainObject.Id.Should().Be(id);
        }

        [TestCase(TestName = "Get and Set plant Name property works properly")]
        public void GetAndSetNameWorksProperly()
        {
            // Given
            string name = Guid.Empty.ToString();

            // When
            DomainObject.Name = name;

            // Then
            DomainObject.Name.Should().Be(name);
        }

        [TestCase(TestName = "Get and Set plant Species property works properly")]
        public void GetAndSetSpeciesWorksProperly()
        {
            // Given
            Species species = new Species
            {
                Name = "Pepper",
                LatinName = "Capsicum Annuum"
            };

            // When
            DomainObject.Species = species;

            // Then
            DomainObject.Species.Should().Be(species);
        }

        [TestCase(TestName = "Get and Set plant field property works properly")]
        public void GetAndSetFieldWorksProperly()
        {
            // Given
            Field field = New.Field.WithName("Random field #1");

            // When
            DomainObject.Field = field;

            // Then
            DomainObject.Field.Should().Be(field);
        }

        [TestCase(TestName = "Get and Set plant Row property works properly")]
        public void GetAndSetRowWorksProperly()
        {
            // Given
            int row = 1;

            // When
            DomainObject.Row = row;

            // Then
            DomainObject.Row.Should().Be(row);
        }

        [TestCase(TestName = "Get and Set plant Column property works properly")]
        public void GetAndSetColumnWorksProperly()
        {
            // Given
            int column = 2;

            // When
            DomainObject.Column = column;

            // Then
            DomainObject.Column.Should().Be(column);
        }

        [TestCase(TestName = "Get and Set plant Planted property works properly")]
        public void GetAndSetPlantedWorksProperly()
        {
            // Given
            DateTime planted = DateTime.Now.AddDays(1);

            // When
            DomainObject.Planted = planted;

            // Then
            DomainObject.Planted.Day.Should().Be(planted.Day);
        }

        [TestCase(TestName = "Get and Set plant State property works properly")]
        public void GetAndSetStateWorksProperly()
        {
            // Given
            PlantState state = PlantState.Deceased;

            // When
            DomainObject.State = state;

            // Then
            DomainObject.State.Should().Be(state);
        }

        [TestCase(TestName = "Get and Set administered nutrients collection property works properly")]
        public void GetAndSetAdministeredNutrientsWorksProperly()
        {
            // Given
            ICollection<AdministeredNutrient> administeredNutrients = new List<AdministeredNutrient>();

            // When
            DomainObject.AdministeredNutrients = administeredNutrients;

            // Then
            DomainObject.AdministeredNutrients.Should().BeSameAs(administeredNutrients);
        }

        [TestCase(TestName = "Get and Set events collection property works properly")]
        public void GetAndSetEventsWorksProperly()
        {
            // Given
            ICollection<Event> events = new List<Event>();

            // When
            DomainObject.Events = events;

            // Then
            DomainObject.Events.Should().BeSameAs(events);
        }

        [TestCase(TestName = "Create plant should produce creation event")]
        public void CreatePlantShouldProduceCreationEvent()
        {
            // Given
            string plantName = "Plant";
            Species species = new Species();
            Field field = New.Field.WithName("Test field");
            int row = 0;
            int col = 0;

            // When
            Plant plant = new Plant(plantName, species, field, row, col);

            // Then
            plant.Events.Should().Contain(x => x.Type == EventType.Start);
        }

        [TestCase(TestName = "Change field should produce field changed event")]
        public void ChangeFieldShouldProduceFieldChangedEvent()
        {
            // Given
            string plantName = "Plant";
            Species species = new Species();
            Field field = New.Field.WithName("Test field #1");
            int row = 0;
            int col = 0;
            Plant plant = new Plant(plantName, species, field, row, col);
            Field newField = New.Field.WithName("Test field #2");

            // When
            plant.ChangeField(newField);

            // Then
            plant.Events.Should().Contain(x => x.Type == EventType.Moving);
        }
    }
}