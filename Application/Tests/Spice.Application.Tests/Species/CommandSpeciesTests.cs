using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Species;
using Spice.Application.Species.Exceptions;
using Spice.Application.Species.Models;
using Spice.Application.Tests.Common.Base;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Species
{
    [TestFixture]
    internal sealed class CommandSpeciesTests : AbstractInMemoryDatabaseAwareTestFixture
    {
        private CommandSpecies _commands;

        [SetUp]
        public void SetUp()
        {
            DatabaseContext = SetupInMemoryDatabase();
            DatabaseContext.Database.EnsureCreated();
            _commands = new CommandSpecies(DatabaseContext, Mapper);
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseContext.Database.EnsureDeleted();
        }

        [TestCase(TestName = "Create species throws exception if species with specified name already exists")]
        public void CreateSpeciesThrowsExceptionOnNameConflict()
        {
            // Given
            Domain.Plants.Species existingSpecies = ModelFactory.DomainModel();
            SeedDatabase(existingSpecies);
            CreateSpeciesModel model = ModelFactory.CreationModel();

            // When
            Func<Task> createSpecies = async () => await _commands.Create(model);

            // Then
            createSpecies.Should().Throw<SpeciesWithNameAlreadyExistsException>();
        }

        [TestCase(TestName = "Create species returns Guid on success")]
        public async Task CreateSpeciesReturnsGuidOnSuccess()
        {
            // Given
            CreateSpeciesModel model = ModelFactory.CreationModel();

            // When
            Guid SpeciesId = await _commands.Create(model);

            // Then
            SpeciesId.Should().NotBe(Guid.Empty);
        }

        [TestCase(TestName = "Update species throws exception if species with specified name already exists")]
        public void UpdateSpeciesThrowsExceptionOnNameConflict()
        {
            // Given
            Domain.Plants.Species existingSpecies = ModelFactory.DomainModel();
            SeedDatabase(existingSpecies);
            Domain.Plants.Species updatedSpecies = ModelFactory.DomainModel("Species B");
            Guid updatedSpeciesId = SeedDatabase(updatedSpecies);
            UpdateSpeciesModel model = ModelFactory.UpdateModel(updatedSpeciesId);

            // When
            Func<Task> updateSpecies = async () => await _commands.Update(model);

            // Then
            updateSpecies.Should().Throw<SpeciesWithNameAlreadyExistsException>();
        }

        [TestCase(TestName = "Update species returns null if species does not exist")]
        public async Task UpdateSpeciesReturnsNullIfSpeciesDoesNotExist()
        {
            // Given
            UpdateSpeciesModel model = ModelFactory.UpdateModel(Guid.NewGuid());

            // When
            Domain.Plants.Species species = await _commands.Update(model);

            // Then
            species.Should().BeNull();
        }

        [TestCase(TestName = "Update species returns updated species on success")]
        public async Task UpdateSpeciesReturnsSpeciesOnSuccess()
        {
            // Given
            Domain.Plants.Species species = ModelFactory.DomainModel("Basil");
            Guid speciesId = SeedDatabase(species);
            UpdateSpeciesModel model = ModelFactory.UpdateModel(speciesId);

            // When
            species = await _commands.Update(model);

            // Then
            species.Should().NotBeNull();
            species.Name.Should().Be("Pepper");
        }

        [TestCase(TestName = "Delete species succeeds")]
        public async Task DeleteSpeciesSucceeds()
        {
            // Given
            Guid speciesId = SeedDatabase(ModelFactory.DomainModel("Species B"));

            // When
            await _commands.Delete(speciesId);

            // Then
            Domain.Plants.Species species = await DatabaseContext.Species.FindAsync(speciesId);
            species.Should().BeNull();
        }
    }
}