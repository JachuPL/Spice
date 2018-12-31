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
        [TestCase(TestName = "Get and Set plant Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            Plant plant = new Plant();
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
            Plant plant = new Plant();
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
            Plant plant = new Plant();
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
            Plant plant = new Plant();
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
            Plant plant = new Plant();
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
            Plant plant = new Plant();
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
            Plant plant = new Plant();
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
            Plant plant = new Plant();
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
            Plant plant = new Plant();
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
            Plant plant = new Plant();
            ICollection<Event> events = new List<Event>();

            // When
            plant.Events = events;

            // Then
            plant.Events.Should().BeSameAs(events);
        }
    }
}