using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Nutrients;
using Spice.Application.Nutrients.Exceptions;
using Spice.Application.Nutrients.Models;
using Spice.Application.Tests.Common.Base;
using Spice.Domain;
using Spice.Domain.Plants;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Nutrients
{
    internal sealed class CommandNutrientsTests : AbstractInMemoryDatabaseAwareTestFixture
    {
        private CommandNutrients _commands;

        [SetUp]
        public void SetUp()
        {
            DatabaseContext = SetupInMemoryDatabase();
            DatabaseContext.Database.EnsureCreated();
            _commands = new CommandNutrients(DatabaseContext, Mapper);
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseContext.Database.EnsureDeleted();
        }

        [TestCase(TestName = "Create nutrient throws exception if nutrient with specified name already exists")]
        public void CreateNutrientThrowsExceptionOnNameConflict()
        {
            // Given
            Nutrient existingNutrient = ModelFactory.DomainModel();
            SeedDatabase(existingNutrient);
            CreateNutrientModel model = ModelFactory.CreationModel();

            // When
            Func<Task> createNutrient = async () => await _commands.Create(model);

            // Then
            createNutrient.Should().Throw<NutrientWithNameAlreadyExistsException>();
        }

        [TestCase(TestName = "Create nutrient returns Guid on success")]
        public async Task CreateNutrientReturnsGuidOnSuccess()
        {
            // Given
            CreateNutrientModel model = ModelFactory.CreationModel();

            // When
            Guid NutrientId = await _commands.Create(model);

            // Then
            NutrientId.Should().NotBe(Guid.Empty);
        }

        [TestCase(TestName = "Update nutrient throws exception if nutrient with specified name already exists")]
        public void UpdateNutrientThrowsExceptionOnNameConflict()
        {
            // Given
            Nutrient existingNutrient = ModelFactory.DomainModel();
            SeedDatabase(existingNutrient);
            Nutrient updatedNutrient = ModelFactory.DomainModel("Nutrient B");
            Guid updatedNutrientId = SeedDatabase(updatedNutrient);
            UpdateNutrientModel model = ModelFactory.UpdateModel(updatedNutrientId);

            // When
            Func<Task> updateNutrient = async () => await _commands.Update(model);

            // Then
            updateNutrient.Should().Throw<NutrientWithNameAlreadyExistsException>();
        }

        [TestCase(TestName = "Update nutrient returns null if nutrient does not exist")]
        public async Task UpdateNutrientReturnsNullIfNutrientDoesNotExist()
        {
            // Given
            UpdateNutrientModel model = ModelFactory.UpdateModel(Guid.NewGuid());

            // When
            Nutrient nutrient = await _commands.Update(model);

            // Then
            nutrient.Should().BeNull();
        }

        [TestCase(TestName = "Update nutrient throws exception if nutrient was already administered to any plant")]
        public void UpdateNutrientThrowsExceptionIfItWasAdministeredToAnyPlant()
        {
            // Given
            Nutrient nutrient = ModelFactory.DomainModel("Nutrient B");
            Guid nutrientId = SeedDatabase(nutrient);
            Plant plant = Plants.ModelFactory.DomainModel();
            SeedDatabase(plant);
            AdministeredNutrient administeredNutrient = Plants.Nutrients.ModelFactory.DomainModel(nutrient, plant);
            SeedDatabase(administeredNutrient);
            UpdateNutrientModel model = ModelFactory.UpdateModel(nutrientId);

            // When
            Func<Task> updateNutrient = async () => await _commands.Update(model);

            // Then
            updateNutrient.Should().Throw<NutrientAdministeredToAPlantException>();
        }

        [TestCase(TestName = "Update nutrient returns updated nutrient on success")]
        public async Task UpdateNutrientReturnsNutrientOnSuccess()
        {
            // Given
            Nutrient nutrient = ModelFactory.DomainModel("Nutrient B", dosageUnits: "ml");
            Guid nutrientId = SeedDatabase(nutrient);
            UpdateNutrientModel model = ModelFactory.UpdateModel(nutrientId);
            model.DosageUnits = "g";

            // When
            nutrient = await _commands.Update(model);

            // Then
            nutrient.Should().NotBeNull();
            nutrient.Name.Should().Be("Nutrient A");
            nutrient.DosageUnits.Should().Be("g");
        }

        [TestCase(TestName = "Delete nutrient succeeds")]
        public async Task DeleteNutrientSucceeds()
        {
            // Given
            Guid nutrientId = SeedDatabase(ModelFactory.DomainModel("Nutrient B", dosageUnits: "g"));

            // When
            await _commands.Delete(nutrientId);

            // Then
            Nutrient nutrient = await DatabaseContext.Nutrients.FindAsync(nutrientId);
            nutrient.Should().BeNull();
        }
    }
}