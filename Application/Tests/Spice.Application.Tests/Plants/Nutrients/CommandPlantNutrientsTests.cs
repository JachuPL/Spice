using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Nutrients.Exceptions;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Nutrients;
using Spice.Application.Plants.Nutrients.Exceptions;
using Spice.Application.Plants.Nutrients.Models;
using Spice.Application.Tests.Common.Base;
using Spice.Domain;
using Spice.Domain.Plants;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Plants.Nutrients
{
    internal sealed class CommandPlantNutrientsTests : AbstractInMemoryDatabaseAwareTestFixture
    {
        private CommandPlantNutrients _commands;

        [SetUp]
        public void SetUp()
        {
            DatabaseContext = SetupInMemoryDatabase();
            DatabaseContext.Database.EnsureCreated();
            _commands = new CommandPlantNutrients(DatabaseContext, Mapper);
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseContext.Database.EnsureDeleted();
        }

        [TestCase(TestName = "Create administered plant nutrition throws exception if plant does not exist")]
        public void CreateAdministeredNutrientThrowsExceptionIfPlantDoesNotExist()
        {
            // Given
            CreateAdministeredNutrientModel model = ModelFactory.CreationModel(Guid.NewGuid());

            // When
            Func<Task> createPlant = async () => await _commands.Create(Guid.NewGuid(), model);

            // Then
            createPlant.Should().Throw<PlantNotFoundException>();
        }

        [TestCase(TestName = "Create administered plant nutrition throws exception if nutrient does not exist")]
        public void CreateAdministeredNutrientThrowsExceptionIfNutrientDoesNotExist()
        {
            // Given
            Guid plantId = SeedDatabase(Plants.ModelFactory.DomainModel());
            CreateAdministeredNutrientModel model = ModelFactory.CreationModel();

            // When
            Func<Task> createPlant = async () => await _commands.Create(plantId, model);

            // Then
            createPlant.Should().Throw<NutrientNotFoundException>();
        }

        [TestCase(TestName = "Create administered plant nutrition throws exception if administration date is earlier than plant date")]
        public void CreateAdministeredNutrientThrowsExceptionIfAdministrationDateIsEarlierThanPlantDate()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            Guid plantId = SeedDatabase(plant);
            Guid nutrientId = SeedDatabase(Tests.Nutrients.ModelFactory.DomainModel());
            CreateAdministeredNutrientModel model = ModelFactory.CreationModel(nutrientId, plant.Planted.AddDays(-1));

            // When
            Func<Task> createPlant = async () => await _commands.Create(plantId, model);

            // Then
            createPlant.Should().Throw<NutrientAdministrationDateBeforePlantDateException>();
        }

        [TestCase(TestName = "Create administered plant nutrition returns Guid on success")]
        public async Task CreateAdministeredNutrientReturnsGuidOnSuccess()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            plant.Planted = DateTime.Now.AddDays(-1);
            Guid plantId = SeedDatabase(plant);
            Guid nutrientId = SeedDatabase(Tests.Nutrients.ModelFactory.DomainModel());
            CreateAdministeredNutrientModel model = ModelFactory.CreationModel(nutrientId);

            // When
            Guid id = await _commands.Create(plantId, model);

            // Then
            id.Should().NotBe(Guid.Empty);
        }

        [TestCase(TestName = "Update administered plant nutrition throws exception if plant does not exist")]
        public void UpdateAdministeredNutrientThrowsExceptionIfPlantDoesNotExist()
        {
            // Given
            UpdateAdministeredNutrientModel model = ModelFactory.UpdateModel(Guid.NewGuid());

            // When
            Func<Task> updatePlant = async () => await _commands.Update(Guid.NewGuid(), model);

            // Then
            updatePlant.Should().Throw<PlantNotFoundException>();
        }

        [TestCase(TestName = "Update administered plant nutrition throws exception if nutrient does not exist")]
        public void UpdateAdministeredNutrientThrowsExceptionIfNutrientDoesNotExist()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            Guid plantId = SeedDatabase(plant);
            AdministeredNutrient administeredNutrient = ModelFactory.DomainModel(plant: plant);
            Guid administeredNutrientId = SeedDatabase(administeredNutrient);
            UpdateAdministeredNutrientModel model = ModelFactory.UpdateModel(administeredNutrientId, Guid.NewGuid());

            // When
            Func<Task> updatePlant = async () => await _commands.Update(plantId, model);

            // Then
            updatePlant.Should().Throw<NutrientNotFoundException>();
        }

        [TestCase(TestName = "Update administered plant nutrition throws exception if administration date is earlier than plant date")]
        public void UpdateAdministeredNutrientThrowsExceptionIfAdministrationDateIsEarlierThanPlantDate()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            Guid plantId = SeedDatabase(plant);
            Nutrient nutrient = Tests.Nutrients.ModelFactory.DomainModel();
            Guid nutrientId = SeedDatabase(nutrient);
            AdministeredNutrient administeredNutrient = ModelFactory.DomainModel(nutrient, plant);
            Guid administeredNutrientId = SeedDatabase(administeredNutrient);
            UpdateAdministeredNutrientModel model = ModelFactory.UpdateModel(administeredNutrientId, nutrientId, plant.Planted.AddDays(-1));

            // When
            Func<Task> updatePlant = async () => await _commands.Update(plantId, model);

            // Then
            updatePlant.Should().Throw<NutrientAdministrationDateBeforePlantDateException>();
        }

        [TestCase(TestName = "Update administered plant nutrition returns null if administered nutrient does not exist")]
        public async Task UpdateAdministeredNutrientReturnsNullIfAdministeredNutrientDoesNotExist()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            Guid plantId = SeedDatabase(plant);
            UpdateAdministeredNutrientModel model = ModelFactory.UpdateModel();

            // When
            AdministeredNutrient administeredNutrient = await _commands.Update(plantId, model);

            // Then
            administeredNutrient.Should().BeNull();
        }

        [TestCase(TestName = "Update administered plant nutrient returns updated administered plant nutrient on success")]
        public async Task UpdateAdministeredNutrientReturnsAdministeredNutrientOnSuccess()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            Guid plantId = SeedDatabase(plant);
            Nutrient nutrient = Tests.Nutrients.ModelFactory.DomainModel();
            Guid nutrientId = SeedDatabase(nutrient);
            AdministeredNutrient administeredNutrient = ModelFactory.DomainModel(plant: plant);
            Guid administeredNutrientId = SeedDatabase(administeredNutrient);

            UpdateAdministeredNutrientModel model = ModelFactory.UpdateModel(administeredNutrientId, nutrientId);

            // When
            administeredNutrient = await _commands.Update(plantId, model);

            // Then
            administeredNutrient.Should().NotBeNull();
            administeredNutrient.Id.Should().Be(administeredNutrientId);
            administeredNutrient.Plant.Id.Should().Be(plant.Id);
            administeredNutrient.Nutrient.Id.Should().Be(nutrient.Id);
        }

        [TestCase(TestName = "Delete administered plant nutrient succeeds")]
        public async Task DeleteAdministeredNutrientSucceeds()
        {
            // Given
            Plant plant = Plants.ModelFactory.DomainModel();
            Guid plantId = SeedDatabase(plant);
            AdministeredNutrient administeredNutrient = ModelFactory.DomainModel(plant: plant);
            Guid id = SeedDatabase(administeredNutrient);

            // When
            await _commands.Delete(plantId, id);

            // Then
            administeredNutrient = await DatabaseContext.AdministeredNutrients.FindAsync(id);
            administeredNutrient.Should().BeNull();
        }
    }
}