using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spice.Application.Fields.Exceptions;
using Spice.Application.Plants;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Models;
using Spice.AutoMapper;
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
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _service = SetupInMemoryDatabase();
            _service.Database.EnsureCreated();
            _mapper = AutoMapperFactory.CreateMapper();
            _commands = new CommandPlants(_service, _mapper);
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
            Field field = Fields.ModelFactory.DomainModel();
            Guid fieldId = SeedDatabase(field);
            SeedDatabase(ModelFactory.DomainModel(field));
            CreatePlantModel model = ModelFactory.CreationModel(fieldId);

            // When
            Func<Task> createPlant = async () => await _commands.Create(model);

            // Then
            createPlant.Should().Throw<PlantExistsAtCoordinatesException>();
        }

        [TestCase(TestName = "Create plant throws exception if field with specified id does not exist")]
        public void CreatePlantThrowsExceptionIfFieldDoesNotExist()
        {
            // Given
            SeedDatabase(ModelFactory.DomainModel());
            CreatePlantModel model = ModelFactory.CreationModel();

            // When
            Func<Task> createPlant = async () => await _commands.Create(model);

            // Then
            createPlant.Should().Throw<FieldDoesNotExistException>();
        }

        private Guid SeedDatabase(Plant plant)
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                plant.Field = ctx.Fields.Find(plant.Field.Id);
                ctx.Plants.Add(plant);
                ctx.Save();

                return plant.Id;
            }
        }

        private Guid SeedDatabase(Field field)
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                ctx.Fields.Add(field);
                ctx.Save();

                return field.Id;
            }
        }

        [TestCase(TestName = "Create plant returns Guid on success")]
        public async Task CreatePlantReturnsGuidOnSuccess()
        {
            // Given
            Guid fieldId = SeedDatabase(Fields.ModelFactory.DomainModel());
            CreatePlantModel model = ModelFactory.CreationModel(fieldId);

            // When
            Guid id = await _commands.Create(model);

            // Then
            id.Should().NotBe(Guid.Empty);
        }

        [TestCase(TestName = "Update plant throws exception if plant exists on same fields and coordinates")]
        public void UpdatePlantThrowsExceptionIfPlantExistsAtCoordinates()
        {
            // Given
            Field field = Fields.ModelFactory.DomainModel();
            Guid fieldId = SeedDatabase(field);
            SeedDatabase(ModelFactory.DomainModel(field));   // existing plant on existing field
            Guid id = SeedDatabase(ModelFactory.DomainModel(field));
            UpdatePlantModel model = ModelFactory.UpdateModel(id, fieldId);

            // When
            Func<Task> updatePlant = async () => await _commands.Update(model);

            // Then
            updatePlant.Should().Throw<PlantExistsAtCoordinatesException>();
        }

        [TestCase(TestName = "Update plant throws exception if field with specified id does not exist")]
        public void UpdatePlantThrowsExceptionIfFieldDoesNotExist()
        {
            // Given
            SeedDatabase(ModelFactory.DomainModel());   // existing plant
            Guid id = SeedDatabase(ModelFactory.DomainModel());
            UpdatePlantModel model = ModelFactory.UpdateModel(id);

            // When
            Func<Task> updatePlant = async () => await _commands.Update(model);

            // Then
            updatePlant.Should().Throw<FieldDoesNotExistException>();
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
            Field field = Fields.ModelFactory.DomainModel();
            Guid fieldId = SeedDatabase(field);
            Plant plant = ModelFactory.DomainModel(field, 13, 37);
            Guid plantId = SeedDatabase(plant);

            UpdatePlantModel model = ModelFactory.UpdateModel(plantId, fieldId);

            // When
            plant = await _commands.Update(model);

            // Then
            plant.Should().NotBeNull();
            plant.Id.Should().Be(plantId);
            plant.Name.Should().Contain("Red");
            plant.Field.Should().NotBeNull();
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