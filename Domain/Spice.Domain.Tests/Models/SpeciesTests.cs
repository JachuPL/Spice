﻿using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Builders;
using Spice.Domain.Plants;
using System;
using System.Collections.Generic;

namespace Spice.Domain.Tests.Models
{
    [TestFixture]
    internal sealed class SpeciesTests : AbstractBaseDomainTestFixture<Species>
    {
        protected override Species CreateDomainObject() => New
                                                           .Species.WithName("Pepper").WithLatinName("Capsicum Annuum")
                                                           .WithDescription("Completely lacks spiciness.");

        [TestCase(TestName = "Get and Set species Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            Guid id = Guid.NewGuid();

            // When
            DomainObject.Id = id;

            // Then
            DomainObject.Id.Should().Be(id);
        }

        [TestCase(TestName = "Get and Set species Name property works properly")]
        public void GetAndSetNameWorksProperly()
        {
            // Given
            string name = Guid.Empty.ToString();

            // When
            DomainObject.Name = name;

            // Then
            DomainObject.Name.Should().Be(name);
        }

        [TestCase(TestName = "Get and Set species Latin Name property works properly")]
        public void GetAndSetLatinNameWorksProperly()
        {
            // Given
            string name = Guid.Empty.ToString();

            // When
            DomainObject.Name = name;

            // Then
            DomainObject.Name.Should().Be(name);
        }

        [TestCase(TestName = "Get and Set species Description property works properly")]
        public void GetAndSetDescriptionWorksProperly()
        {
            // Given
            string description = Guid.Empty.ToString();

            // When
            DomainObject.Description = description;

            // Then
            DomainObject.Description.Should().Be(description);
        }

        [TestCase(TestName = "Get and Set species Plants property works properly")]
        public void GetAndSetPlantsWorksProperly()
        {
            // Given
            List<Plant> plants = new List<Plant>();
            Plant examplePlant = new Plant("Random plant #1", DomainObject,
                                           New.Field.WithName("Plant Setter Test"), 0, 0);

            plants.Add(examplePlant);

            // When
            DomainObject.Plants = plants;

            // Then
            DomainObject.Plants.Should().Contain(examplePlant);
        }
    }
}