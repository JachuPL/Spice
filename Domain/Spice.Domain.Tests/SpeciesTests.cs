using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Plants;
using System;
using System.Collections.Generic;

namespace Spice.Domain.Tests
{
    [TestFixture]
    internal sealed class SpeciesTests
    {
        [TestCase(TestName = "Get and Set species Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            Species species = new Species();
            Guid id = Guid.NewGuid();

            // When
            species.Id = id;

            // Then
            species.Id.Should().Be(id);
        }

        [TestCase(TestName = "Get and Set species Name property works properly")]
        public void GetAndSetNameWorksProperly()
        {
            // Given
            Species species = new Species();
            string name = Guid.Empty.ToString();

            // When
            species.Name = name;

            // Then
            species.Name.Should().Be(name);
        }

        [TestCase(TestName = "Get and Set species Latin Name property works properly")]
        public void GetAndSetLatinNameWorksProperly()
        {
            // Given
            Species species = new Species();
            string name = Guid.Empty.ToString();

            // When
            species.Name = name;

            // Then
            species.Name.Should().Be(name);
        }

        [TestCase(TestName = "Get and Set species Description property works properly")]
        public void GetAndSetDescriptionWorksProperly()
        {
            // Given
            Species species = new Species();
            string description = Guid.Empty.ToString();

            // When
            species.Description = description;

            // Then
            species.Description.Should().Be(description);
        }

        [TestCase(TestName = "Get and Set species Plants property works properly")]
        public void GetAndSetPlantsWorksProperly()
        {
            // Given
            Species species = new Species();
            List<Plant> plants = new List<Plant>();
            Plant examplePlant = new Plant("Random plant #1", new Species(), new Field(), 0, 0);

            plants.Add(examplePlant);

            // When
            species.Plants = plants;

            // Then
            species.Plants.Should().Contain(examplePlant);
        }
    }
}