using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Nutrients;
using Spice.Application.Nutrients.Exceptions;
using Spice.Application.Nutrients.Models;
using Spice.Application.Tests.Common.Base;
using Spice.Domain;
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

        [TestCase(TestName = "Create Nutrient throws exception if nutrient with specified name already exists")]
        public void CreateNutrientThrowsExceptionOnNameConflict()
        {
            // Given
            Nutrient existingNutrient = ModelFactory.DomainModel();
            Guid existingNutrientId = SeedDatabase(existingNutrient);
            CreateNutrientModel model = ModelFactory.CreationModel();

            // When
            Func<Task> createNutrient = async () => await _commands.Create(model);

            // Then
            createNutrient.Should().Throw<NutrientWithNameAlreadyExistsException>();
        }

        [TestCase(TestName = "Create Nutrient returns Guid on success")]
        public async Task CreateNutrientReturnsGuidOnSuccess()
        {
            // Given
            CreateNutrientModel model = ModelFactory.CreationModel();

            // When
            Guid NutrientId = await _commands.Create(model);

            // Then
            NutrientId.Should().NotBe(Guid.Empty);
        }

        [TestCase(TestName = "Update Nutrient throws exception if nutrient with specified name already exists")]
        public void UpdateNutrientThrowsExceptionOnNameConflict()
        {
            // Given
            Nutrient existingNutrient = ModelFactory.DomainModel();
            Guid existingNutrientId = SeedDatabase(existingNutrient);
            Nutrient updatedNutrient = ModelFactory.DomainModel("Nutrient B");
            Guid updatedNutrientId = SeedDatabase(updatedNutrient);
            UpdateNutrientModel model = ModelFactory.UpdateModel(updatedNutrientId);

            // When
            Func<Task> updateNutrient = async () => await _commands.Update(model);

            // Then
            updateNutrient.Should().Throw<NutrientWithNameAlreadyExistsException>();
        }

        [TestCase(TestName = "Update Nutrient throws exception if nutrient does not exist")]
        public void UpdateNutrientThrowsExceptionIfNutrientDoesNotExist()
        {
            // Given
            UpdateNutrientModel model = ModelFactory.UpdateModel(Guid.NewGuid());

            // When
            Func<Task> updateNutrient = async () => await _commands.Update(model);

            // Then
            updateNutrient.Should().Throw<NutrientDoesNotExistException>();
        }

        [TestCase(TestName = "Update Nutrient returns updated nutrient on success")]
        public async Task UpdateNutrientReturnsNutrientOnSuccess()
        {
            // Given
            Nutrient Nutrient = ModelFactory.DomainModel("Nutrient B", dosageUnits: "ml");
            Guid NutrientId = SeedDatabase(Nutrient);
            UpdateNutrientModel model = ModelFactory.UpdateModel(NutrientId);
            model.DosageUnits = "g";

            // When
            Nutrient = await _commands.Update(model);

            // Then
            Nutrient.Should().NotBeNull();
            Nutrient.Name.Should().Be("Nutrient A");
            Nutrient.DosageUnits.Should().Be("g");
        }

        [TestCase(TestName = "Delete Nutrient succeeds")]
        public async Task DeleteNutrientShouldSucceed()
        {
            // Given
            Guid NutrientId = SeedDatabase(ModelFactory.DomainModel("Nutrient B", dosageUnits: "g"));

            // When
            await _commands.Delete(NutrientId);

            // Then
            Nutrient Nutrient = await DatabaseContext.Nutrients.FindAsync(NutrientId);
            Nutrient.Should().BeNull();
        }
    }
}