using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Plants;
using System;
using System.Collections.Generic;

namespace Spice.Domain.Tests.Models
{
    [TestFixture]
    internal sealed class FieldTests : AbstractBaseDomainTestFixture<Field>
    {
        protected override Field CreateDomainObject() => new Field
        {
            Name = "Test",
            Description = "Test desc",
            Latitude = 52,
            Longtitude = 21
        };

        [TestCase(TestName = "Get and Set field Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            Guid id = Guid.NewGuid();

            // When
            DomainObject.Id = id;

            // Then
            DomainObject.Id.Should().Be(id);
        }

        [TestCase(TestName = "Get and Set field Name property works properly")]
        public void GetAndSetNameWorksProperly()
        {
            // Given
            string name = Guid.Empty.ToString();

            // When
            DomainObject.Name = name;

            // Then
            DomainObject.Name.Should().Be(name);
        }

        [TestCase(TestName = "Get and Set field Description property works properly")]
        public void GetAndSetDescriptionWorksProperly()
        {
            // Given
            string description = Guid.Empty.ToString();

            // When
            DomainObject.Description = description;

            // Then
            DomainObject.Description.Should().Be(description);
        }

        [TestCase(TestName = "Get and Set field Latitude property works properly")]
        public void GetAndSetLatitudeWorksProperly()
        {
            // Given
            double latitude = 52.123456;

            // When
            DomainObject.Latitude = latitude;

            // Then
            DomainObject.Latitude.Should().Be(latitude);
        }

        [TestCase(TestName = "Get and Set field Longtitude property works properly")]
        public void GetAndSetLongtitudeWorksProperly()
        {
            // Given
            double longtitude = 52.123456;

            // When
            DomainObject.Longtitude = longtitude;

            // Then
            DomainObject.Longtitude.Should().Be(longtitude);
        }

        [TestCase(TestName = "Get and Set field Plants property works properly")]
        public void GetAndSetPlantsWorksProperly()
        {
            // Given
            List<Plant> plants = new List<Plant>();
            Plant examplePlant = new Plant("Random plant #1", new Species(), new Field(), 0, 0);

            plants.Add(examplePlant);

            // When
            DomainObject.Plants = plants;

            // Then
            DomainObject.Plants.Should().Contain(examplePlant);
        }
    }
}