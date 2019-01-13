using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Builders;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using System;

namespace Spice.Domain.Tests.Models.Plants
{
    [TestFixture]
    internal sealed class EventTests : AbstractBaseDomainTestFixture<Event>
    {
        private readonly Plant _eventOwnerPlant = New.Plant
                                                     .WithName("Test plant")
                                                     .WithSpecies(New.Species.WithName("Test species"))
                                                     .WithField(New.Field.WithName("Plant event tests"))
                                                     .InRow(0)
                                                     .InColumn(0);

        protected override Event CreateDomainObject() =>
            _eventOwnerPlant.AddEvent(EventType.Disease, "Spotted some brown leaves.");

        [TestCase(TestName = "Get and Set event Id property works properly")]
        public void GetAndSetIdWorksProperly()
        {
            // Given
            Guid id = Guid.NewGuid();

            // When
            DomainObject.Id = id;

            // Then
            DomainObject.Id.Should().Be(id);
        }

        [TestCase(TestName = "Get and Set event Plant property works properly")]
        public void GetAndSetPlantWorksProperly()
        {
            // Given
            Plant plant = _eventOwnerPlant;

            // When
            DomainObject.Plant = plant;

            // Then
            DomainObject.Plant.Should().Be(plant);
        }

        [TestCase(TestName = "Get and Set event type property works properly")]
        public void GetAndSetTypeWorksProperly()
        {
            // Given
            EventType type = EventType.Growth;

            // When
            DomainObject.Type = type;

            // Then
            DomainObject.Type.Should().Be(type);
        }

        [TestCase(TestName = "Get and Set event description property works properly")]
        public void GetAndSetDescriptionWorksProperly()
        {
            // Given
            string description = "Random event description";

            // When
            DomainObject.Description = description;

            // Then
            DomainObject.Description.Should().Be(description);
        }

        [TestCase(TestName = "Get and Set event Occured property works properly")]
        public void GetAndSetOccuredWorksProperly()
        {
            // Given
            DateTime date = DateTime.Now.AddDays(1);

            // When
            DomainObject.Occured = date;

            // Then
            DomainObject.Occured.Should().Be(date);
        }
    }
}