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
        private Event CreateTestEvent() =>
            new Event(new Plant("Test", new Species(), new Field(), 0, 0), EventType.Disease, "Test");

        [TestCase(TestName = "Get and Set event Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            Event @event = CreateTestEvent();
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
            Event @event = CreateTestEvent();
            Plant plant = new Plant("Test", new Species(), new Field(), 0, 0);

            // When
            @event.Plant = plant;

            // Then
            @event.Plant.Should().Be(plant);
        }

        [TestCase(TestName = "Get and Set event type property works properly")]
        public void GetAndSetTypeWorksProperly()
        {
            // Given
            Event @event = CreateTestEvent();
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
            Event @event = CreateTestEvent();
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
            Event @event = CreateTestEvent();
            DateTime date = DateTime.Now.AddDays(1);

            // When
            @event.Occured = date;

            // Then
            @event.Occured.Should().Be(date);
        }
    }
}