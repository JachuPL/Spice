using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Plants.Nutrients;
using Spice.Application.Plants.Nutrients.Models;
using Spice.Application.Tests.Common.Base;
using Spice.Domain;
using Spice.Domain.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Plants.Nutrients
{
    [TestFixture]
    internal sealed class QueryPlantNutrientsTests : AbstractInMemoryDatabaseAwareTestFixture
    {
        private QueryPlantNutrients _queries;

        [SetUp]
        public void SetUp()
        {
            DatabaseContext = SetupInMemoryDatabase();
            DatabaseContext.Database.EnsureCreated();
            _queries = new QueryPlantNutrients(DatabaseContext, Mapper);
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseContext.Database.EnsureDeleted();
        }

        [TestCase(TestName = "Get all by plant id query on plants returns administered nutrients")]
        public async Task GetAllByPlantReturnsAdministeredNutrients()
        {
            // Given
            Guid plantId = SeedDatabaseForGetByPlantIdTesting();

            // When
            IEnumerable<AdministeredNutrient> administeredNutrients = await _queries.GetByPlant(plantId);

            // Then
            administeredNutrients.Should().NotBeNull();
        }

        private Guid SeedDatabaseForGetByPlantIdTesting()
        {
            Plant plant = Plants.ModelFactory.DomainModel();
            AdministeredNutrient administeredNutrient1 = ModelFactory.DomainModel();
            AdministeredNutrient administeredNutrient2 = ModelFactory.DomainModel();
            AdministeredNutrient administeredNutrient3 = ModelFactory.DomainModel();

            plant.AdministeredNutrients.Add(administeredNutrient1);
            plant.AdministeredNutrients.Add(administeredNutrient2);
            plant.AdministeredNutrients.Add(administeredNutrient3);
            DatabaseContext.Plants.Add(plant);
            DatabaseContext.AdministeredNutrients.Add(administeredNutrient1);
            DatabaseContext.AdministeredNutrients.Add(administeredNutrient2);
            DatabaseContext.AdministeredNutrients.Add(administeredNutrient3);

            DatabaseContext.Save();
            return plant.Id;
        }

        [TestCase(TestName = "Get all by plant id query on plant nutrients returns null if plant does not exist")]
        public async Task GetAllByPlantReturnsNullIfPlantDoesNotExist()
        {
            // Given
            Guid plantId = Guid.NewGuid();

            // When
            IEnumerable<AdministeredNutrient> queryNutrients = await _queries.GetByPlant(plantId);

            // Then
            queryNutrients.Should().BeNull();
        }

        [TestCase(TestName = "Get by plant id query on plant nutrients returns null if nutrient was not administered on plant")]
        public async Task GetByPlantReturnsNullWhenNotFound()
        {
            // Given
            Plant plant = SeedDatabaseForGetByPlantTesting();

            // When
            AdministeredNutrient administeredNutrient = await _queries.Get(plant.Id, Guid.NewGuid());

            // Then
            administeredNutrient.Should().BeNull();
        }

        [TestCase(TestName = "Get by plant id query on plant nutrients returns null if plant does not exist")]
        public async Task GetByPlantReturnsNullIfPlantDoesNotExist()
        {
            // Given
            Guid plantId = Guid.NewGuid();
            Guid id = Guid.NewGuid();

            // When
            AdministeredNutrient administeredNutrient = await _queries.Get(plantId, id);

            // Then
            administeredNutrient.Should().BeNull();
        }

        [TestCase(TestName = "Get by plant id query on plant nutrients returns plant if found")]
        public async Task GetByPlantReturnsAdministeredNutrients()
        {
            // Given
            Plant plant = SeedDatabaseForGetByPlantTesting();
            AdministeredNutrient administeredNutrient = plant.AdministeredNutrients.First();

            // When
            AdministeredNutrient administeredNutrientFromDatabase = await _queries.Get(plant.Id, administeredNutrient.Id);

            // Then
            administeredNutrientFromDatabase.Should().NotBeNull();
        }

        private Plant SeedDatabaseForGetByPlantTesting()
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                Plant plant = Plants.ModelFactory.DomainModel();
                AdministeredNutrient administeredNutrient = ModelFactory.DomainModel();
                plant.AdministeredNutrients.Add(administeredNutrient);
                ctx.Plants.Add(plant);
                ctx.Save();
                return plant;
            }
        }

        [TestCase(TestName = "Get summary of administered nutrients by plant id returns null if plant does not exist")]
        public async Task AdministeredNutrientsSummaryReturnsNullIfPlantDoesNotExist()
        {
            // Given
            Guid plantId = Guid.NewGuid();

            // When
            IEnumerable<PlantNutrientAdministrationCountModel> nutrientsSummary = await _queries.Summary(plantId);

            // Then
            nutrientsSummary.Should().BeNull();
        }

        [TestCase(TestName = "Get summary of administered nutrients by plant id returns administered nutrients summary")]
        public async Task AdministeredNutrientsSummaryReturnsAdministeredNutrientsSummary()
        {
            // Given
            Plant plant = SeedDatabaseForGetNutrientSummaryTesting();

            // When
            IEnumerable<PlantNutrientAdministrationCountModel> administeredNutrientFromDatabase =
                await _queries.Summary(plant.Id);

            // Then
            administeredNutrientFromDatabase.Should().NotBeNullOrEmpty();
            PlantNutrientAdministrationCountModel waterSummary =
                administeredNutrientFromDatabase.Single(x => x.Nutrient.Name == "Water");
            waterSummary.Nutrient.Should().NotBeNull();
            waterSummary.TotalAmount.Should().Be(2);

            PlantNutrientAdministrationCountModel fertilizerSummary =
                administeredNutrientFromDatabase.Single(x => x.Nutrient.Name == "Fertilizer");
            fertilizerSummary.Nutrient.Should().NotBeNull();
            fertilizerSummary.TotalAmount.Should().Be(1);
        }

        private Plant SeedDatabaseForGetNutrientSummaryTesting()
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                Plant plant = Plants.ModelFactory.DomainModel();
                Nutrient nutrient1 = new Nutrient() { Name = "Water" };
                AdministeredNutrient administeredNutrient1 = ModelFactory.DomainModel(nutrient1);
                AdministeredNutrient administeredNutrient2 = ModelFactory.DomainModel(nutrient1);
                Nutrient nutrient2 = new Nutrient() { Name = "Fertilizer" };
                AdministeredNutrient administeredNutrient3 = ModelFactory.DomainModel(nutrient2);
                plant.AdministeredNutrients.Add(administeredNutrient1);
                plant.AdministeredNutrients.Add(administeredNutrient2);
                plant.AdministeredNutrients.Add(administeredNutrient3);
                ctx.Plants.Add(plant);
                ctx.Nutrients.Add(nutrient1);
                ctx.Nutrients.Add(nutrient2);
                ctx.Save();
                return plant;
            }
        }
    }
}