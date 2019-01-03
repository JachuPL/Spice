using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using System;
using System.Collections.Generic;

namespace Spice.Domain.Tests.Plants
{
    [TestFixture]
    internal sealed class PlantsTests
    {
        private Plant CreateTestPlant() => new Plant("Test", new Species(), new Field(), 0, 0);

        [TestCase(TestName = "Get and Set plant Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            Plant plant = CreateTestPlant();
            Guid id = Guid.NewGuid();

            // When
            plant.Id = id;

            // Then
            plant.Id.Should().Be(id);
        }

        [TestCase(TestName = "Get and Set plant Name property works properly")]
        public void GetAndSetNameWorksProperly()
        {
            // Given
            Plant plant = CreateTestPlant();
            string name = Guid.Empty.ToString();

            // When
            plant.Name = name;

            // Then
            plant.Name.Should().Be(name);
        }

        [TestCase(TestName = "Get and Set plant Species property works properly")]
        public void GetAndSetSpeciesWorksProperly()
        {
            // Given
            Plant plant = CreateTestPlant();
            Species species = new Species()
            {
                Name = "Pepper",
                LatinName = "Capsicum Annuum"
            };

            // When
            plant.Species = species;

            // Then
            plant.Species.Should().Be(species);
        }

        [TestCase(TestName = "Get and Set plant field property works properly")]
        public void GetAndSetFieldWorksProperly()
        {
            // Given
            Plant plant = CreateTestPlant();
            Field field = new Field()
            {
                Name = "Random field #1"
            };

            // When
            plant.Field = field;

            // Then
            plant.Field.Should().Be(field);
        }

        [TestCase(TestName = "Get and Set plant Row property works properly")]
        public void GetAndSetRowWorksProperly()
        {
            // Given
            Plant plant = CreateTestPlant();
            int row = 1;

            // When
            plant.Row = row;

            // Then
            plant.Row.Should().Be(row);
        }

        [TestCase(TestName = "Get and Set plant Column property works properly")]
        public void GetAndSetColumnWorksProperly()
        {
            // Given
            Plant plant = CreateTestPlant();
            int column = 2;

            // When
            plant.Column = column;

            // Then
            plant.Column.Should().Be(column);
        }

        [TestCase(TestName = "Get and Set plant Planted property works properly")]
        public void GetAndSetPlantedWorksProperly()
        {
            // Given
            Plant plant = CreateTestPlant();
            DateTime planted = DateTime.Now.AddDays(1);

            // When
            plant.Planted = planted;

            // Then
            plant.Planted.Day.Should().Be(planted.Day);
        }

        [TestCase(TestName = "Get and Set plant State property works properly")]
        public void GetAndSetStateWorksProperly()
        {
            // Given
            Plant plant = CreateTestPlant();
            PlantState state = PlantState.Deceased;

            // When
            plant.State = state;

            // Then
            plant.State.Should().Be(state);
        }

        [TestCase(TestName = "Get and Set administered nutrients collection property works properly")]
        public void GetAndSetAdministeredNutrientsWorksProperly()
        {
            // Given
            Plant plant = CreateTestPlant();
            ICollection<AdministeredNutrient> administeredNutrients = new List<AdministeredNutrient>();

            // When
            plant.AdministeredNutrients = administeredNutrients;

            // Then
            plant.AdministeredNutrients.Should().BeSameAs(administeredNutrients);
        }

        [TestCase(TestName = "Get and Set events collection property works properly")]
        public void GetAndSetEventsWorksProperly()
        {
            // Given
            Plant plant = CreateTestPlant();
            ICollection<Event> events = new List<Event>();

            // When
            plant.Events = events;

            // Then
            plant.Events.Should().BeSameAs(events);
        }

        [TestCase(TestName = "Create plant should produce creation event")]
        public void CreatePlantShouldProduceCreationEvent()
        {
            // Given
            string plantName = "Plant";
            Species species = new Species();
            Field field = new Field();
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
            Field field = new Field();
            int row = 0;
            int col = 0;
            Plant plant = new Plant(plantName, species, field, row, col);
            Field newField = new Field();

            // When
            plant.ChangeField(newField);

            // Then
            plant.Events.Should().Contain(x => x.Type == EventType.Moving);
        }
    }
}