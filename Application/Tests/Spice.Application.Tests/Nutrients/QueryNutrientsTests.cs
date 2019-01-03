using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Nutrients;
using Spice.Application.Nutrients.Models;
using Spice.Application.Tests.Common.Base;
using Spice.Domain;
using Spice.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Nutrients
{
    [TestFixture]
    internal sealed class QueryNutrientsTests : AbstractInMemoryDatabaseAwareTestFixture
    {
        private QueryNutrients _queries;

        [SetUp]
        public void SetUp()
        {
            DatabaseContext = SetupInMemoryDatabase();
            DatabaseContext.Database.EnsureCreated();
            _queries = new QueryNutrients(DatabaseContext, Mapper);
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseContext.Database.EnsureDeleted();
        }

        [TestCase(TestName = "GetAll query on nutrients returns all nutrients")]
        public async Task GetAllReturnsAllNutrients()
        {
            // Given
            SeedDatabaseForGetAllTesting();

            // When
            IEnumerable<Nutrient> nutrients = await _queries.GetAll();

            // Then
            nutrients.Should().NotBeNullOrEmpty();
        }

        private void SeedDatabaseForGetAllTesting()
        {
            using (SpiceContext ctx = SetupInMemoryDatabase())
            {
                ctx.Nutrients.Add(ModelFactory.DomainModel("Nutrient A"));
                ctx.Nutrients.Add(ModelFactory.DomainModel("Nutrient B"));
                ctx.Nutrients.Add(ModelFactory.DomainModel("Nutrient C"));
                ctx.Nutrients.Add(ModelFactory.DomainModel("Nutrient D"));
                ctx.Save();
            }
        }

        [TestCase(TestName = "Get by id query onnutrientsreturns null if nutrient does not exist")]
        public async Task GetNutrientReturnsNullWhenNotFound()
        {
            // Given
            Guid nutrientId = Guid.NewGuid();

            // When
            NutrientDetailsModel nutrient = await _queries.Get(nutrientId);

            // Then
            nutrient.Should().BeNull();
        }

        [TestCase(TestName = "Get by id query on nutrients returns nutrient if found")]
        public async Task GetNutrientReturnsNutrientWhenFound()
        {
            // Given
            Guid nutrientId = SeedDatabaseForGetByIdTesting();

            // When
            NutrientDetailsModel nutrient = await _queries.Get(nutrientId);

            // Then
            nutrient.Should().NotBeNull();
        }

        private Guid SeedDatabaseForGetByIdTesting()
        {
            using (SpiceContext ctx = SetupInMemoryDatabase())
            {
                Nutrient nutrient = ModelFactory.DomainModel();
                ctx.Nutrients.Add(nutrient);
                ctx.Save();
                return nutrient.Id;
            }
        }
    }
}