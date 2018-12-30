using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Plants.Events;
using Spice.Application.Plants.Events.Exceptions;
using Spice.Application.Plants.Events.Models;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Tests.Common.Base;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Plants.Events
{
    internal sealed class CommandPlantEventsTests : AbstractInMemoryDatabaseAwareTestFixture
    {
        private CommandPlantEvents _commands;

        [SetUp]
        public void SetUp()
        {
            DatabaseContext = SetupInMemoryDatabase();
            DatabaseContext.Database.EnsureCreated();
            _commands = new CommandPlantEvents(DatabaseContext, Mapper);
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseContext.Database.EnsureDeleted();
        }

        [TestCase(TestName = "Create plant event throws exception if plant does not exist")]
        public void CreatePlantEventThrowsExceptionIfPlantDoesNotExist()
        {
            // Given
            CreatePlantEventModel model = ModelFactory.CreationModel();

            // When
            Func<Task> createPlant = async () => await _commands.Create(Guid.NewGuid(), model);

            // Then
            createPlant.Should().Throw<PlantDoesNotExistException>();
        }

        [TestCase(TestName = "Create plant event throws exception if occurence date is earlier than plant date")]
        public void CreatePlantEventThrowsExceptionIfOccurenceDateIsEarlierThanPlantDate()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            Guid plantId = SeedDatabase(plant);
            CreatePlantEventModel model = ModelFactory.CreationModel(plant.Planted.AddDays(-1));

            // When
            Func<Task> createPlant = async () => await _commands.Create(plantId, model);

            // Then
            createPlant.Should().Throw<EventOccurenceDateBeforePlantDateOrInTheFutureException>();
        }

        [TestCase(TestName = "Create plant event throws exception if occurence date is in the future")]
        public void CreatePlantEventThrowsExceptionIfOccurenceDateIsInTheFuture()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            Guid plantId = SeedDatabase(plant);
            CreatePlantEventModel model = ModelFactory.CreationModel(DateTime.Now.AddDays(1));

            // When
            Func<Task> createPlant = async () => await _commands.Create(plantId, model);

            // Then
            createPlant.Should().Throw<EventOccurenceDateBeforePlantDateOrInTheFutureException>();
        }

        [TestCase(TestName = "Create plant event returns Guid on success")]
        public async Task CreatePlantEventReturnsGuidOnSuccess()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            plant.Planted = DateTime.Now.AddDays(-1);
            Guid plantId = SeedDatabase(plant);
            CreatePlantEventModel model = ModelFactory.CreationModel();

            // When
            Guid id = await _commands.Create(plantId, model);

            // Then
            id.Should().NotBe(Guid.Empty);
        }

        [TestCase(TestName = "Update plant event throws exception if plant does not exist")]
        public void UpdatePlantEventThrowsExceptionIfPlantDoesNotExist()
        {
            // Given
            UpdatePlantEventModel model = ModelFactory.UpdateModel(Guid.NewGuid());

            // When
            Func<Task> updateEvent = async () => await _commands.Update(Guid.NewGuid(), model);

            // Then
            updateEvent.Should().Throw<PlantDoesNotExistException>();
        }

        [TestCase(TestName = "Update plant event throws exception if occurence date is earlier than plant date")]
        public void UpdatePlantEventThrowsExceptionIfOccurenceDateIsEarlierThanPlantDate()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            Guid plantId = SeedDatabase(plant);
            Event @event = ModelFactory.DomainModel(plant);
            Guid eventId = SeedDatabase(@event);
            UpdatePlantEventModel model = ModelFactory.UpdateModel(eventId, plant.Planted.AddDays(-1));

            // When
            Func<Task> updateEvent = async () => await _commands.Update(plantId, model);

            // Then
            updateEvent.Should().Throw<EventOccurenceDateBeforePlantDateOrInTheFutureException>();
        }

        [TestCase(TestName = "Update plant event throws exception if occurence date is in the future")]
        public void UpdatePlantEventThrowsExceptionIfOccurenceDateIsInTheFuture()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            Guid plantId = SeedDatabase(plant);
            Event @event = ModelFactory.DomainModel(plant);
            Guid eventId = SeedDatabase(@event);
            UpdatePlantEventModel model = ModelFactory.UpdateModel(eventId, DateTime.Now.AddDays(1));

            // When
            Func<Task> updateEvent = async () => await _commands.Update(plantId, model);

            // Then
            updateEvent.Should().Throw<EventOccurenceDateBeforePlantDateOrInTheFutureException>();
        }

        [TestCase(TestName = "Update plant event returns null if occured event does not exist")]
        public async Task UpdatePlantEventReturnsNullIfEventDoesNotExist()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            Guid plantId = SeedDatabase(plant);
            UpdatePlantEventModel model = ModelFactory.UpdateModel();

            // When
            Event @event = await _commands.Update(plantId, model);

            // Then
            @event.Should().BeNull();
        }

        [TestCase(TestName = "Update plant event returns updated plant event on success")]
        public async Task UpdatePlantEventReturnsEventOnSuccess()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            Guid plantId = SeedDatabase(plant);
            Event @event = ModelFactory.DomainModel(plant);
            Guid eventId = SeedDatabase(@event);

            UpdatePlantEventModel model = ModelFactory.UpdateModel(eventId);

            // When
            @event = await _commands.Update(plantId, model);

            // Then
            @event.Should().NotBeNull();
            @event.Id.Should().Be(eventId);
            @event.Plant.Id.Should().Be(plant.Id);
        }

        [TestCase(TestName = "Delete plant event succeeds")]
        public async Task DeletePlantEventSucceeds()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            Guid plantId = SeedDatabase(plant);
            Event @event = ModelFactory.DomainModel(plant);
            Guid id = SeedDatabase(@event);

            // When
            await _commands.Delete(plantId, id);

            // Then
            @event = await DatabaseContext.Events.FindAsync(id);
            @event.Should().BeNull();
        }
    }
}