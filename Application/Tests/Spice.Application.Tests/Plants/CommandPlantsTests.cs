using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spice.Application.Plants;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Models;
using Spice.Domain;
using Spice.Persistence;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Plants
{
    public class CommandPlantsTests
    {
        private CommandPlants _commands;
        private SpiceContext _service;

        [SetUp]
        public void SetUp()
        {
            _service = SetupInMemoryDatabase();
            _service.Database.EnsureCreated();
            _commands = new CommandPlants(_service);
        }

        [TearDown]
        public void TearDown()
        {
            _service.Database.EnsureDeleted();
        }

        private SpiceContext SetupInMemoryDatabase()
        {
            var ctxOptionsBuilder = new DbContextOptionsBuilder<SpiceContext>();
            ctxOptionsBuilder.UseInMemoryDatabase("TestSpiceDatabase");
            return new SpiceContext(ctxOptionsBuilder.Options);
        }

        [TestCase(TestName = "Create plant throws exception if plant exists on same fields and coordinates")]
        public void CreatePlantThrowsExceptionIfPlantExistsAtCoordinates()
        {
            // Given
            SeedDatabase(ModelFactory.DomainModel());
            CreatePlantModel model = ModelFactory.CreationModel();

            // When
            Func<Task> createPlant = async () => await _commands.Create(model);

            // Then
            createPlant.Should().Throw<PlantExistsAtCoordinatesException>();
        }

        private Guid SeedDatabase(Plant plant)
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                ctx.Plants.Add(plant);
                ctx.Save();

                return plant.Id;
            }
        }

        [TestCase(TestName = "Create plant returns Guid on success")]
        public async Task CreatePlantReturnsGuidOnSuccess()
        {
            // Given
            CreatePlantModel model = ModelFactory.CreationModel();

            // When
            Guid id = await _commands.Create(model);

            // Then
            id.Should().NotBe(Guid.Empty);
        }

        [TestCase(TestName = "Update plant throws exception if plant exists on same fields and coordinates")]
        public void UpdatePlantThrowsExceptionIfPlantExistsAtCoordinates()
        {
            // Given
            SeedDatabase(ModelFactory.DomainModel());   // existing plant
            Guid id = SeedDatabase(ModelFactory.DomainModel());
            UpdatePlantModel model = ModelFactory.UpdateModel(id);

            // When
            Func<Task> updatePlant = async () => await _commands.Update(model);

            // Then
            updatePlant.Should().Throw<PlantExistsAtCoordinatesException>();
        }

        [TestCase(TestName = "Update plant returns null if plant does not exist")]
        public async Task UpdatePlantReturnsNullIfPlantDoesNotExist()
        {
            // Given
            UpdatePlantModel model = ModelFactory.UpdateModel();

            // When
            Plant plant = await _commands.Update(model);

            // Then
            plant.Should().BeNull();
        }

        [TestCase(TestName = "Update plant returns updated plant on success")]
        public async Task UpdatePlantReturnsPlantOnSuccess()
        {
            // Given
            Guid id = SeedDatabase(ModelFactory.DomainModel("Field X", 13, 37));
            UpdatePlantModel model = ModelFactory.UpdateModel(id);

            // When
            Plant plant = await _commands.Update(model);

            // Then
            plant.Should().NotBeNull();
            plant.Id.Should().Be(id);
            plant.Name.Should().Contain("Red");
            plant.FieldName.Should().Contain("A");
            plant.Column.Should().Be(0);
            plant.Row.Should().Be(0);
            plant.Planted.Day.Should().Be(DateTime.Now.Day);
            plant.State.Should().Be(PlantState.Healthy);
        }

        [TestCase(TestName = "Delete plant succeeds")]
        public async Task DeletePlantShouldSucceed()
        {
            // Given
            Guid id = SeedDatabase(ModelFactory.DomainModel());

            // When
            await _commands.Delete(id);

            // Then
            Plant plant = await _service.Plants.FindAsync(id);
            plant.Should().BeNull();
        }
    }
}