﻿using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spice.Application.Fields.Exceptions;
using Spice.Application.Plants;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Models;
using Spice.Application.Species.Exceptions;
using Spice.Application.Tests.Common.Base;
using Spice.Domain;
using Spice.Domain.Plants;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Plants
{
    internal sealed class CommandPlantsTests : AbstractInMemoryDatabaseAwareTestFixture
    {
        private CommandPlants _commands;

        [SetUp]
        public void SetUp()
        {
            DatabaseContext = SetupInMemoryDatabase();
            DatabaseContext.Database.EnsureCreated();
            _commands = new CommandPlants(DatabaseContext, Mapper);
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseContext.Database.EnsureDeleted();
        }

        [TestCase(TestName = "Create plant throws exception if plant exists on same field and coordinates")]
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

        [TestCase(TestName = "Create plant throws exception if field does not exist")]
        public void CreatePlantThrowsExceptionIfFieldDoesNotExist()
        {
            // Given
            SeedDatabase(ModelFactory.DomainModel());
            CreatePlantModel model = ModelFactory.CreationModel();

            // When
            Func<Task> createPlant = async () => await _commands.Create(model);

            // Then
            createPlant.Should().Throw<FieldNotFoundException>();
        }

        [TestCase(TestName = "Create plant throws exception if species does not exist")]
        public void CreatePlantThrowsExceptionIfSpeciesDoesNotExist()
        {
            // Given
            Field field = Fields.ModelFactory.DomainModel();
            Guid fieldId = SeedDatabase(field);
            CreatePlantModel model = ModelFactory.CreationModel(fieldId);

            // When
            Func<Task> createPlant = async () => await _commands.Create(model);

            // Then
            createPlant.Should().Throw<SpeciesNotFoundException>();
        }

        [TestCase(TestName = "Create plant creates event and returns Guid on success")]
        public async Task CreatePlantReturnsGuidOnSuccess()
        {
            // Given
            Guid fieldId = SeedDatabase(Fields.ModelFactory.DomainModel());
            Guid speciesId = SeedDatabase(Species.ModelFactory.DomainModel());
            CreatePlantModel model = ModelFactory.CreationModel(fieldId, speciesId);

            // When
            Guid id = await _commands.Create(model);
            Plant createdPlant =
                await DatabaseContext.Plants.Include(x => x.Events).FirstOrDefaultAsync(x => x.Id == id);

            // Then
            id.Should().NotBe(Guid.Empty);
            createdPlant.Events.Should().NotBeNullOrEmpty();
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

        [TestCase(TestName = "Update plant throws exception if field does not exist")]
        public void UpdatePlantThrowsExceptionIfFieldDoesNotExist()
        {
            // Given
            SeedDatabase(ModelFactory.DomainModel());   // existing plant
            Guid id = SeedDatabase(ModelFactory.DomainModel());
            UpdatePlantModel model = ModelFactory.UpdateModel(id);

            // When
            Func<Task> updatePlant = async () => await _commands.Update(model);

            // Then
            updatePlant.Should().Throw<FieldNotFoundException>();
        }

        [TestCase(TestName = "Update plant throws exception if species does not exist")]
        public void UpdatePlantThrowsExceptionIfSpeciesDoesNotExist()
        {
            // Given
            SeedDatabase(ModelFactory.DomainModel());   // existing plant
            Guid id = SeedDatabase(ModelFactory.DomainModel());
            Field field = Fields.ModelFactory.DomainModel();
            Guid fieldId = SeedDatabase(field);
            UpdatePlantModel model = ModelFactory.UpdateModel(id, fieldId);

            // When
            Func<Task> updatePlant = async () => await _commands.Update(model);

            // Then
            updatePlant.Should().Throw<SpeciesNotFoundException>();
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
            Field newField = Fields.ModelFactory.DomainModel();
            Guid newFieldId = SeedDatabase(newField);
            Domain.Species newSpecies = Species.ModelFactory.DomainModel();
            Guid newSpeciesId = SeedDatabase(newSpecies);
            Plant plant = ModelFactory.DomainModel(row: 13, col: 37);
            Guid plantId = SeedDatabase(plant);

            UpdatePlantModel model = ModelFactory.UpdateModel(plantId, newFieldId, newSpeciesId);

            // When
            plant = await _commands.Update(model);

            // Then
            plant.Should().NotBeNull();
            plant.Id.Should().Be(plantId);
            plant.Name.Should().Contain("Red");
            plant.Field.Should().Be(newField);
            plant.Species.Should().Be(newSpecies);
            plant.Column.Should().Be(0);
            plant.Row.Should().Be(0);
            plant.Planted.Day.Should().Be(DateTime.Now.Day);
            plant.State.Should().Be(PlantState.Healthy);
            plant.Events.Should().NotBeNullOrEmpty();
        }

        [TestCase(TestName = "Delete plant succeeds")]
        public async Task DeletePlantSucceeds()
        {
            // Given
            Guid id = SeedDatabase(ModelFactory.DomainModel());

            // When
            await _commands.Delete(id);

            // Then
            Plant plant = await DatabaseContext.Plants.FindAsync(id);
            plant.Should().BeNull();
        }
    }
}