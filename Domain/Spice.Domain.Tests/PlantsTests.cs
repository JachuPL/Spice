using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Plants;
using System;

namespace Spice.Domain.Tests
{
    [TestFixture]
    internal sealed class StateTests
    {
        [TestCase(TestName = "Get and Set field Id property works properly")]
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

        [TestCase(TestName = "Get and Set field Name property works properly")]
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

        [TestCase(TestName = "Get and Set field Specimen property works properly")]
        public void GetAndSetSpecimenWorksProperly()
        {
            // Given
            Plant plant = new Plant();
            string Specimen = Guid.Empty.ToString();

            // When
            plant.Specimen = Specimen;

            // Then
            plant.Specimen.Should().Be(Specimen);
        }

        [TestCase(TestName = "Get and Set field Field property works properly")]
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

        [TestCase(TestName = "Get and Set field Row property works properly")]
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

        [TestCase(TestName = "Get and Set field Column property works properly")]
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

        [TestCase(TestName = "Get and Set field Planted property works properly")]
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

        [TestCase(TestName = "Get and Set field State property works properly")]
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
    }
}