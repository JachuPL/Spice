using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Plants.Events;
using Spice.Application.Plants.Events.Models.Summary;
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

        [TestCase(TestName = "Get all by plant id query on plant events returns null if plant does not exist")]
        public async Task GetAllByPlantReturnsNullIfPlantDoesNotExist()
        {
            // Given
            Guid plantId = Guid.NewGuid();

            // When
            IEnumerable<Event> query = await _queries.GetByPlant(plantId);

            // Then
            query.Should().BeNull();
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

        [TestCase(TestName = "Get by plant id query on plant events returns null if plant does not exist")]
        public async Task GetByPlantReturnsNullIfPlantDoesNotExist()
        {
            // Given
            Guid plantId = Guid.NewGuid();
            Guid id = Guid.NewGuid();

            // When
            Event @event = await _queries.Get(plantId, id);

            // Then
            @event.Should().BeNull();
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

        [TestCase(TestName = "Get summary of occured events by plant id returns null if plant does not exist")]
        public async Task EventsSummaryReturnsNullIfPlantDoesNotExist()
        {
            // Given
            Guid plantId = Guid.NewGuid();

            // When
            IEnumerable<PlantEventOccurenceCountModel> eventsSummary = await _queries.Summary(plantId);

            // Then
            eventsSummary.Should().BeNull();
        }

        [TestCase(TestName = "Get summary of occured events by plant id returns occured events summary from entire history")]
        public async Task EventsSummaryReturnsEventsSummaryFromEntireHistory()
        {
            // Given
            Plant plant = SeedDatabaseForGetEventSummaryTesting();

            // When
            IEnumerable<PlantEventOccurenceCountModel> eventsFromDatabase = await _queries.Summary(plant.Id);

            // Then
            eventsFromDatabase.Should().NotBeNullOrEmpty();
            PlantEventOccurenceCountModel diseaseSummary = eventsFromDatabase.Single(x => x.Type == EventType.Disease);
            diseaseSummary.TotalCount.Should().Be(4);
            diseaseSummary.FirstOccurence.Should().Be(new DateTime(2018, 01, 01, 0, 0, 0));
            diseaseSummary.LastOccurence.Should().Be(new DateTime(2018, 04, 01, 0, 0, 0));
            PlantEventOccurenceCountModel overwateringSummary = eventsFromDatabase.Single(x => x.Type == EventType.OverWatering);
            overwateringSummary.TotalCount.Should().Be(1);
            overwateringSummary.FirstOccurence.Should().Be(new DateTime(2018, 01, 01, 0, 0, 0));
            overwateringSummary.LastOccurence.Should().Be(overwateringSummary.FirstOccurence);
        }

        [TestCase(TestName = "Get summary of occured events by plant id returns occured events summary from specified date range")]
        public async Task EventsSummaryReturnsEventsSummaryFromSpecifiedDateRange()
        {
            // Given
            Plant plant = SeedDatabaseForGetEventSummaryTesting();

            // When
            IEnumerable<PlantEventOccurenceCountModel> eventsFromDatabase = await _queries.Summary(plant.Id,
                new DateTime(2018, 1, 1, 0, 0, 0),
                new DateTime(2018, 3, 1, 23, 59, 59));

            // Then
            eventsFromDatabase.Should().NotBeNullOrEmpty();
            PlantEventOccurenceCountModel diseaseSummary = eventsFromDatabase.Single(x => x.Type == EventType.Disease);
            diseaseSummary.TotalCount.Should().Be(3);
            diseaseSummary.FirstOccurence.Should().Be(new DateTime(2018, 01, 01, 0, 0, 0));
            diseaseSummary.LastOccurence.Should().Be(new DateTime(2018, 03, 01, 0, 0, 0));
            PlantEventOccurenceCountModel overwateringSummary = eventsFromDatabase.Single(x => x.Type == EventType.OverWatering);
            overwateringSummary.TotalCount.Should().Be(1);
            overwateringSummary.FirstOccurence.Should().Be(new DateTime(2018, 01, 01, 0, 0, 0));
            overwateringSummary.LastOccurence.Should().Be(overwateringSummary.FirstOccurence);
        }

        private Plant SeedDatabaseForGetEventSummaryTesting()
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                Plant plant = Plants.ModelFactory.DomainModel();
                Event event1 = ModelFactory.DomainModel(plant, EventType.Disease, new DateTime(2018, 1, 1, 0, 0, 0));
                Event event2 = ModelFactory.DomainModel(plant, EventType.Disease, new DateTime(2018, 2, 1, 0, 0, 0));
                Event event3 = ModelFactory.DomainModel(plant, EventType.Disease, new DateTime(2018, 3, 1, 0, 0, 0));
                Event event4 = ModelFactory.DomainModel(plant, EventType.Disease, new DateTime(2018, 4, 1, 0, 0, 0));
                Event event5 = ModelFactory.DomainModel(plant, EventType.OverWatering, new DateTime(2018, 1, 1, 0, 0, 0));
                plant.Events.Add(event1);
                plant.Events.Add(event2);
                plant.Events.Add(event3);
                plant.Events.Add(event4);
                plant.Events.Add(event5);
                ctx.Plants.Add(plant);
                ctx.Events.Add(event1);
                ctx.Events.Add(event2);
                ctx.Events.Add(event3);
                ctx.Events.Add(event4);
                ctx.Events.Add(event5);
                ctx.Save();
                return plant;
            }
        }
    }
}