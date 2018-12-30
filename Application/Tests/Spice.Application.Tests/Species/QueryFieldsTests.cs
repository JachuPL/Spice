using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Species;
using Spice.Application.Tests.Common.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Species
{
    [TestFixture]
    internal sealed class QuerySpeciesTests : AbstractInMemoryDatabaseAwareTestFixture
    {
        private QuerySpecies _queries;

        [SetUp]
        public void SetUp()
        {
            DatabaseContext = SetupInMemoryDatabase();
            DatabaseContext.Database.EnsureCreated();
            _queries = new QuerySpecies(DatabaseContext);
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseContext.Database.EnsureDeleted();
        }

        [TestCase(TestName = "GetAll query on species returns all species")]
        public async Task GetAllReturnsAllSpecies()
        {
            // Given
            SeedDatabaseForGetAllTesting();

            // When
            IEnumerable<Domain.Plants.Species> species = await _queries.GetAll();

            // Then
            species.Should().NotBeNullOrEmpty();
        }

        private void SeedDatabaseForGetAllTesting()
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                ctx.Species.Add(Species.ModelFactory.DomainModel("Species A"));
                ctx.Species.Add(Species.ModelFactory.DomainModel("Species B"));
                ctx.Species.Add(Species.ModelFactory.DomainModel("Species C"));
                ctx.Species.Add(Species.ModelFactory.DomainModel("Species D"));
                ctx.Save();
            }
        }

        [TestCase(TestName = "Get by id query on species returns null if species does not exist")]
        public async Task GetSpeciesReturnsNullWhenNotFound()
        {
            // Given
            Guid speciesId = Guid.NewGuid();

            // When
            Domain.Plants.Species species = await _queries.Get(speciesId);

            // Then
            species.Should().BeNull();
        }

        [TestCase(TestName = "Get by id query on species returns species if found")]
        public async Task GetSpeciesReturnsSpeciesWhenFound()
        {
            // Given
            Guid SpeciesId = SeedDatabaseForGetByIdTesting();

            // When
            Domain.Plants.Species species = await _queries.Get(SpeciesId);

            // Then
            species.Should().NotBeNull();
        }

        private Guid SeedDatabaseForGetByIdTesting()
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                Domain.Plants.Species species = ModelFactory.DomainModel();
                ctx.Species.Add(species);
                ctx.Save();
                return species.Id;
            }
        }
    }
}