using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Plants.Events;
using Spice.Application.Plants.Events.Models;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Tests.Common.Base;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Plants.Events
{
    [TestFixture]
    internal sealed class QueryPlantEventsTests : AbstractInMemoryDatabaseAwareTestFixture
    {
        private QueryPlantEvents _queries;

        [SetUp]
        public void SetUp()
        {
            DatabaseContext = SetupInMemoryDatabase();
            DatabaseContext.Database.EnsureCreated();
            _queries = new QueryPlantEvents(DatabaseContext, Mapper);
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseContext.Database.EnsureDeleted();
        }

        [TestCase(TestName = "Get all by plant id query on plant events returns all events")]
        public async Task GetAllByPlantReturnsEvents()
        {
            // Given
            Guid plantId = SeedDatabaseForGetByPlantIdTesting();

            // When
            IEnumerable<Event> events = await _queries.GetByPlant(plantId);

            // Then
            events.Should().NotBeNullOrEmpty();
        }

        private Guid SeedDatabaseForGetByPlantIdTesting()
        {
            Plant plant = Plants.ModelFactory.DomainModel();
            Event event1 = ModelFactory.DomainModel(plant);
            Event event2 = ModelFactory.DomainModel(plant);
            Event event3 = ModelFactory.DomainModel(plant);

            plant.Events.Add(event1);
            plant.Events.Add(event2);
            plant.Events.Add(event3);
            DatabaseContext.Plants.Add(plant);
            DatabaseContext.Events.Add(event1);
            DatabaseContext.Events.Add(event2);
            DatabaseContext.Events.Add(event3);

            DatabaseContext.Save();
            return plant.Id;
        }

        [TestCase(TestName = "Get all by plant id query on plant events throws exception if plant does not exist")]
        public void GetAllByPlantThrowsExceptionIfPlantDoesNotExist()
        {
            // Given
            Guid plantId = Guid.NewGuid();

            // When
            Func<Task<IEnumerable<Event>>> queryEvents = async () => await _queries.GetByPlant(plantId);

            // Then
            queryEvents.Should().Throw<PlantDoesNotExistException>();
        }

        [TestCase(TestName = "Get by plant id query on plant events returns null if event has not occured on plant")]
        public async Task GetByPlantReturnsNullWhenNotFound()
        {
            // Given
            Plant plant = SeedDatabaseForGetByPlantTesting();

            // When
            Event @event = await _queries.Get(plant.Id, Guid.NewGuid());

            // Then
            @event.Should().BeNull();
        }

        [TestCase(TestName = "Get by plant id query on plant events throws exception if plant does not exist")]
        public void GetByPlantThrowsExceptionIfPlantDoesNotExist()
        {
            // Given
            Guid plantId = Guid.NewGuid();
            Guid id = Guid.NewGuid();

            // When
            Func<Task<Event>> queryForData = async () => await _queries.Get(plantId, id);

            // Then
            queryForData.Should().Throw<PlantDoesNotExistException>();
        }

        [TestCase(TestName = "Get by plant id query on plant events returns plant event if found")]
        public async Task GetByPlantReturnsEventsWhenFound()
        {
            // Given
            Plant plant = SeedDatabaseForGetByPlantTesting();
            Event @event = plant.Events.First();

            // When
            Event EventFromDatabase = await _queries.Get(plant.Id, @event.Id);

            // Then
            EventFromDatabase.Should().NotBeNull();
        }

        private Plant SeedDatabaseForGetByPlantTesting()
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                Plant plant = Plants.ModelFactory.DomainModel();
                Event @event = ModelFactory.DomainModel();
                plant.Events.Add(@event);
                ctx.Plants.Add(plant);
                ctx.Save();
                return plant;
            }
        }

        [TestCase(TestName = "Get summary of occured events by plant id throws exception if plant does not exist")]
        public void SumEventsThrowExceptionIfPlantDoesNotExist()
        {
            // Given
            Guid plantId = Guid.NewGuid();

            // When
            Func<Task<IEnumerable<OccuredPlantEventsSummaryModel>>> queryForData = async () =>
                await _queries.Sum(plantId);

            // Then
            queryForData.Should().Throw<PlantDoesNotExistException>();
        }

        [TestCase(TestName = "Get summary of occured events by plant id returns occured events summary")]
        public async Task SumEventsReturnsEventsSummary()
        {
            // Given
            Plant plant = SeedDatabaseForGetEventSummaryTesting();

            // When
            IEnumerable<OccuredPlantEventsSummaryModel> eventsFromDatabase = await _queries.Sum(plant.Id);

            // Then
            eventsFromDatabase.Should().NotBeNull();
            eventsFromDatabase.Single(x => x.Type == EventType.Disease).TotalCount.Should().Be(2);
            eventsFromDatabase.Single(x => x.Type == EventType.OverWatering).TotalCount.Should().Be(1);
        }

        private Plant SeedDatabaseForGetEventSummaryTesting()
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                Plant plant = Plants.ModelFactory.DomainModel();
                Event event1 = ModelFactory.DomainModel(plant, EventType.Disease);
                Event event2 = ModelFactory.DomainModel(plant, EventType.Disease);
                Event event3 = ModelFactory.DomainModel(plant, EventType.OverWatering);
                plant.Events.Add(event1);
                plant.Events.Add(event2);
                plant.Events.Add(event3);
                ctx.Plants.Add(plant);
                ctx.Events.Add(event1);
                ctx.Events.Add(event2);
                ctx.Events.Add(event3);
                ctx.Save();
                return plant;
            }
        }
    }
}