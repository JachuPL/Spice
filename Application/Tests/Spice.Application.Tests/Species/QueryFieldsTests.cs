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

        [TestCase(TestName = "GetAll query on Species returns all Species")]
        public async Task GetAllReturnsAllSpecies()
        {
            // Given
            SeedDatabaseForGetAllTesting();

            // When
            IEnumerable<Domain.Plants.Species> Species = await _queries.GetAll();

            // Then
            Species.Should().NotBeNullOrEmpty();
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

        [TestCase(TestName = "Get by id query on Species returns null if Species was not found")]
        public async Task GetSpeciesReturnsNullWhenNotFound()
        {
            // When
            Domain.Plants.Species Species = await _queries.Get(Guid.NewGuid());

            // Then
            Species.Should().BeNull();
        }

        [TestCase(TestName = "Get by id query on Species returns Species if found")]
        public async Task GetSpeciesReturnsSpeciesWhenFound()
        {
            // Given
            Guid SpeciesId = SeedDatabaseForGetByIdTesting();

            // When
            Domain.Plants.Species Species = await _queries.Get(SpeciesId);

            // Then
            Species.Should().NotBeNull();
        }

        private Guid SeedDatabaseForGetByIdTesting()
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                Domain.Plants.Species Species = ModelFactory.DomainModel();
                ctx.Species.Add(Species);
                ctx.Save();
                return Species.Id;
            }
        }
    }
}