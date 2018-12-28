using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Nutrients;
using Spice.Application.Nutrients.Models;
using Spice.Application.Tests.Common.Base;
using Spice.Domain;
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

        [TestCase(TestName = "GetAll query on Nutrients returns all nutrient")]
        public async Task GetAllReturnsAllNutrients()
        {
            // Given
            SeedDatabaseForGetAllTesting();

            // When
            IEnumerable<Nutrient> Nutrients = await _queries.GetAll();

            // Then
            Nutrients.Should().NotBeNullOrEmpty();
        }

        private void SeedDatabaseForGetAllTesting()
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                ctx.Nutrients.Add(Nutrients.ModelFactory.DomainModel("Nutrient A"));
                ctx.Nutrients.Add(Nutrients.ModelFactory.DomainModel("Nutrient B"));
                ctx.Nutrients.Add(Nutrients.ModelFactory.DomainModel("Nutrient C"));
                ctx.Nutrients.Add(Nutrients.ModelFactory.DomainModel("Nutrient D"));
                ctx.Save();
            }
        }

        [TestCase(TestName = "Get by id query on Nutrients returns null if nutrient was not found")]
        public async Task GetNutrientReturnsNullWhenNotFound()
        {
            // When
            NutrientDetailsModel Nutrient = await _queries.Get(Guid.NewGuid());

            // Then
            Nutrient.Should().BeNull();
        }

        [TestCase(TestName = "Get by id query on Nutrients returns nutrient if found")]
        public async Task GetNutrientReturnsNutrientWhenFound()
        {
            // Given
            Guid NutrientId = SeedDatabaseForGetByIdTesting();

            // When
            NutrientDetailsModel Nutrient = await _queries.Get(NutrientId);

            // Then
            Nutrient.Should().NotBeNull();
        }

        private Guid SeedDatabaseForGetByIdTesting()
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                Nutrient Nutrient = Nutrients.ModelFactory.DomainModel();
                ctx.Nutrients.Add(Nutrient);
                ctx.Save();
                return Nutrient.Id;
            }
        }
    }
}