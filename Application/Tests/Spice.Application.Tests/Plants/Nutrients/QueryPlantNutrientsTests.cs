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

        [TestCase(TestName = "Get summary of administered nutrients by plant id returns administered nutrients summary from entire history")]
        public async Task AdministeredNutrientsSummaryReturnsAdministeredNutrientsSummaryFromEntireHistory()
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
            waterSummary.TotalAmount.Should().Be(6);
            waterSummary.FirstAdministration.Should().Be(new DateTime(2018, 1, 1, 0, 0, 0));
            waterSummary.LastAdministration.Should().Be(new DateTime(2019, 1, 1, 0, 0, 0));

            PlantNutrientAdministrationCountModel fertilizerSummary =
                administeredNutrientFromDatabase.Single(x => x.Nutrient.Name == "Fertilizer");
            fertilizerSummary.Nutrient.Should().NotBeNull();
            fertilizerSummary.TotalAmount.Should().Be(1);
            fertilizerSummary.FirstAdministration.Should().Be(new DateTime(2018, 1, 1, 0, 0, 0));
            fertilizerSummary.LastAdministration.Should().Be(fertilizerSummary.FirstAdministration);
        }

        [TestCase(TestName = "Get summary of administered nutrients by plant id returns administered nutrients summary from specified date range")]
        public async Task AdministeredNutrientsSummaryReturnsAdministeredNutrientsSummaryFromSpecifiedDateRange()
        {
            // Given
            Plant plant = SeedDatabaseForGetNutrientSummaryTesting();

            // When
            IEnumerable<PlantNutrientAdministrationCountModel> administeredNutrientFromDatabase =
                await _queries.Summary(plant.Id,
                    new DateTime(2018, 1, 1, 0, 0, 0),
                    new DateTime(2018, 4, 1, 23, 59, 59));

            // Then
            administeredNutrientFromDatabase.Should().NotBeNullOrEmpty();
            PlantNutrientAdministrationCountModel waterSummary =
                administeredNutrientFromDatabase.Single(x => x.Nutrient.Name == "Water");
            waterSummary.Nutrient.Should().NotBeNull();
            waterSummary.TotalAmount.Should().Be(4);
            waterSummary.FirstAdministration.Should().Be(new DateTime(2018, 1, 1, 0, 0, 0));
            waterSummary.LastAdministration.Should().Be(new DateTime(2018, 4, 1, 0, 0, 0));

            PlantNutrientAdministrationCountModel fertilizerSummary =
                administeredNutrientFromDatabase.Single(x => x.Nutrient.Name == "Fertilizer");
            fertilizerSummary.Nutrient.Should().NotBeNull();
            fertilizerSummary.TotalAmount.Should().Be(1);
            fertilizerSummary.FirstAdministration.Should().Be(new DateTime(2018, 1, 1, 0, 0, 0));
            fertilizerSummary.LastAdministration.Should().Be(fertilizerSummary.FirstAdministration);
        }

        private Plant SeedDatabaseForGetNutrientSummaryTesting()
        {
            Plant plant = Plants.ModelFactory.DomainModel();
            Nutrient water = new Nutrient() { Name = "Water" };
            AdministeredNutrient administeredWater1 =
                ModelFactory.DomainModel(water, date: new DateTime(2018, 1, 1, 0, 0, 0));
            AdministeredNutrient administeredWater2 =
                ModelFactory.DomainModel(water, date: new DateTime(2018, 2, 1, 0, 0, 0));
            AdministeredNutrient administeredWater3 =
                ModelFactory.DomainModel(water, date: new DateTime(2018, 3, 1, 0, 0, 0));
            AdministeredNutrient administeredWater4 =
                ModelFactory.DomainModel(water, date: new DateTime(2018, 4, 1, 0, 0, 0));
            AdministeredNutrient administeredWater5 =
                ModelFactory.DomainModel(water, date: new DateTime(2018, 5, 1, 0, 0, 0));
            AdministeredNutrient administerdWater6 =
                ModelFactory.DomainModel(water, date: new DateTime(2019, 1, 1, 0, 0, 0));
            Nutrient fertilizer = new Nutrient() { Name = "Fertilizer" };
            AdministeredNutrient administeredFertilizer =
                ModelFactory.DomainModel(fertilizer, date: new DateTime(2018, 1, 1, 0, 0, 0));
            plant.AdministeredNutrients.Add(administeredWater1);
            plant.AdministeredNutrients.Add(administeredWater2);
            plant.AdministeredNutrients.Add(administeredWater3);
            plant.AdministeredNutrients.Add(administeredWater4);
            plant.AdministeredNutrients.Add(administeredWater5);
            plant.AdministeredNutrients.Add(administerdWater6);
            plant.AdministeredNutrients.Add(administeredFertilizer);
            DatabaseContext.Plants.Add(plant);
            DatabaseContext.Nutrients.Add(water);
            DatabaseContext.Nutrients.Add(fertilizer);
            DatabaseContext.Save();
            return plant;
        }
    }
}