using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Plants;
using System;
using System.Collections.Generic;

namespace Spice.Domain.Tests
{
    [TestFixture]
    internal sealed class FieldTests
    {
        [TestCase(TestName = "Get and Set field Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            Field field = new Field();
            Guid id = Guid.NewGuid();

            // When
            field.Id = id;

            // Then
            field.Id.Should().Be(id);
        }

        [TestCase(TestName = "Get and Set field Name property works properly")]
        public void GetAndSetNameWorksProperly()
        {
            // Given
            Field field = new Field();
            string name = Guid.Empty.ToString();

            // When
            field.Name = name;

            // Then
            field.Name.Should().Be(name);
        }

        [TestCase(TestName = "Get and Set field Description property works properly")]
        public void GetAndSetDescriptionWorksProperly()
        {
            // Given
            Field field = new Field();
            string description = Guid.Empty.ToString();

            // When
            field.Description = description;

            // Then
            field.Description.Should().Be(description);
        }

        [TestCase(TestName = "Get and Set field Latitude property works properly")]
        public void GetAndSetLatitudeWorksProperly()
        {
            // Given
            Field field = new Field();
            double latitude = 52.123456;

            // When
            field.Latitude = latitude;

            // Then
            field.Latitude.Should().Be(latitude);
        }

        [TestCase(TestName = "Get and Set field Longtitude property works properly")]
        public void GetAndSetLongtitudeWorksProperly()
        {
            // Given
            Field field = new Field();
            double longtitude = 52.123456;

            // When
            field.Longtitude = longtitude;

            // Then
            field.Longtitude.Should().Be(longtitude);
        }

        [TestCase(TestName = "Get and Set field Plants property works properly")]
        public void GetAndSetPlantsWorksProperly()
        {
            // Given
            Field field = new Field();
            List<Plant> plants = new List<Plant>();
            Plant examplePlant = new Plant("Random plant #1", new Species(), new Field(), 0, 0);

            plants.Add(examplePlant);

            // When
            field.Plants = plants;

            // Then
            field.Plants.Should().Contain(examplePlant);
        }
    }
}