using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using System;

namespace Spice.Domain.Tests.Plants
{
    [TestFixture]
    internal sealed class EventTests
    {
        [TestCase(TestName = "Get and Set event Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            Event @event = new Event();
            Guid id = Guid.NewGuid();

            // When
            @event.Id = id;

            // Then
            @event.Id.Should().Be(id);
        }

        [TestCase(TestName = "Get and Set event Plant property works properly")]
        public void GetAndSetPlantWorksProperly()
        {
            // Given
            Event @event = new Event();
            Plant plant = new Plant();

            // When
            @event.Plant = plant;

            // Then
            @event.Plant.Should().Be(plant);
        }

        [TestCase(TestName = "Get and Set event type property works properly")]
        public void GetAndSetTypeWorksProperly()
        {
            // Given
            Event @event = new Event();
            EventType type = EventType.Growth;

            // When
            @event.Type = type;

            // Then
            @event.Type.Should().Be(type);
        }

        [TestCase(TestName = "Get and Set event description property works properly")]
        public void GetAndSetDescriptionWorksProperly()
        {
            // Given
            Event @event = new Event();
            string description = "Random event description";

            // When
            @event.Description = description;

            // Then
            @event.Description.Should().Be(description);
        }

        [TestCase(TestName = "Get and Set event Occured property works properly")]
        public void GetAndSetOccuredWorksProperly()
        {
            // Given
            Event @event = new Event();
            DateTime date = DateTime.Now.AddDays(1);

            // When
            @event.Occured = date;

            // Then
            @event.Occured.Should().Be(date);
        }
    }
}