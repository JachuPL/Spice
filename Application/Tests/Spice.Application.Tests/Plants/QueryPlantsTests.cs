using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spice.Application.Plants;
using Spice.Domain;
using Spice.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Plants
{
    [TestFixture]
    public class QueryPlantsTests
    {
        private QueryPlants _queries;
        private SpiceContext _service;

        [SetUp]
        public void SetUp()
        {
            _service = SetupInMemoryDatabase();
            _service.Database.EnsureCreated();
            _queries = new QueryPlants(_service);
        }

        [TearDown]
        public void TearDown()
        {
            _service.Database.EnsureDeleted();
        }

        private SpiceContext SetupInMemoryDatabase()
        {
            var ctxOptionsBuilder = new DbContextOptionsBuilder<SpiceContext>();
            ctxOptionsBuilder.UseInMemoryDatabase("TestSpiceDatabase");
            return new SpiceContext(ctxOptionsBuilder.Options);
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
            _service.Plants.Add(new Plant()
            {
                Name = "Rocoto Giant Red",
                Specimen = "Capsicum annuum",
                FieldName = "Field A",
                Column = 0,
                Row = 0,
                Planted = DateTime.Now,
                State = PlantState.Healthy
            });
            _service.Plants.Add(new Plant()
            {
                Name = "Rocoto Giant Yellow",
                Specimen = "Capsicum annuum",
                FieldName = "Field A",
                Column = 1,
                Row = 0,
                Planted = DateTime.Now,
                State = PlantState.Healthy
            });
            _service.Save();
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
                Plant plant = new Plant()
                {
                    Name = "Rocoto Giant Red",
                    Specimen = "Capsicum annuum",
                    FieldName = "Field A",
                    Column = 0,
                    Row = 0,
                    Planted = DateTime.Now,
                    State = PlantState.Healthy
                };
                ctx.Plants.Add(plant);
                ctx.Save();
                return plant.Id;
            }
        }
    }
}