using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Plants;
using Spice.Application.Tests.Common.Base;
using Spice.Domain;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
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
            Domain.Species species = Species.ModelFactory.DomainModel();
            DatabaseContext.Plants.Add(ModelFactory.DomainModel(field));
            DatabaseContext.Plants.Add(ModelFactory.DomainModel(field, species, 0, 1));
            DatabaseContext.Save();
        }

        [TestCase(TestName = "Get by id query on plants returns null if plant does not exist")]
        public async Task GetPlantReturnsNullWhenNotFound()
        {
            // Given
            Guid plantId = Guid.NewGuid();

            // When
            Plant plant = await _queries.Get(plantId);

            // Then
            plant.Should().BeNull();
        }

        [TestCase(TestName = "Get by id query on plants returns plant with nutrition info, field, species and event records")]
        public async Task GetPlantReturnsPlantWhenFound()
        {
            // Given
            Guid id = SeedDatabaseForGetByIdTesting();

            // When
            Plant plant = await _queries.Get(id);

            // Then
            plant.Should().NotBeNull();
            plant.Field.Should().NotBeNull();
            plant.Species.Should().NotBeNull();
            plant.AdministeredNutrients.Should().NotBeNullOrEmpty();
            plant.Events.Should().NotBeNullOrEmpty();
        }

        private Guid SeedDatabaseForGetByIdTesting()
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                Field field = Fields.ModelFactory.DomainModel();
                ctx.Fields.Add(field);

                Domain.Species species = Species.ModelFactory.DomainModel();
                ctx.Species.Add(species);

                Plant plant = ModelFactory.DomainModel(field, species);

                Nutrient nutrient = Tests.Nutrients.ModelFactory.DomainModel();
                ctx.Nutrients.Add(nutrient);

                AdministeredNutrient administeredNutrient = Nutrients.ModelFactory.DomainModel(nutrient, plant);
                plant.AdministeredNutrients.Add(administeredNutrient);
                ctx.AdministeredNutrients.Add(administeredNutrient);

                Event @event = Events.ModelFactory.DomainModel(plant);
                plant.Events.Add(@event);
                ctx.Events.Add(@event);
                ctx.Plants.Add(plant);
                ctx.Save();
                return plant.Id;
            }
        }
    }
}