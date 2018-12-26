using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Plants;
using Spice.Application.Tests.Common.Base;
using Spice.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Plants
{
    [TestFixture]
    internal sealed class QueryPlantsTests : AbstractInMemoryDatabaseAwareTestFixture
    {
        private QueryPlants _queries;

        [SetUp]
        public void SetUp()
        {
            DatabaseContext = SetupInMemoryDatabase();
            DatabaseContext.Database.EnsureCreated();
            _queries = new QueryPlants(DatabaseContext);
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseContext.Database.EnsureDeleted();
        }

        [TestCase(TestName = "GetAll query on plants returns all plants")]
        public async Task GetAllReturnsAllPlants()
        {
            // Given
            SeedDatabaseForGetAllTesting();

            // When
            IEnumerable<Plant> plants = await _queries.GetAll();

            // Then
            plants.Should().NotBeNull();
        }

        private void SeedDatabaseForGetAllTesting()
        {
            Field field = Fields.ModelFactory.DomainModel();
            DatabaseContext.Plants.Add(ModelFactory.DomainModel(field));
            DatabaseContext.Plants.Add(ModelFactory.DomainModel(field, 0, 1));
            DatabaseContext.Save();
        }

        [TestCase(TestName = "Get by id query on plants returns null if plant was not found")]
        public async Task GetPlantReturnsNullWhenNotFound()
        {
            // When
            Plant plant = await _queries.Get(Guid.NewGuid());

            // Then
            plant.Should().BeNull();
        }

        [TestCase(TestName = "Get by id query on plants returns plant if found")]
        public async Task GetPlantReturnsPlantWhenFound()
        {
            // Given
            Guid id = SeedDatabaseForGetByIdTesting();

            // When
            Plant plant = await _queries.Get(id);

            // Then
            plant.Should().NotBeNull();
        }

        private Guid SeedDatabaseForGetByIdTesting()
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                Field field = Fields.ModelFactory.DomainModel();
                Plant plant = ModelFactory.DomainModel(field);
                ctx.Plants.Add(plant);
                ctx.Fields.Add(field);
                ctx.Save();
                return plant.Id;
            }
        }
    }
}