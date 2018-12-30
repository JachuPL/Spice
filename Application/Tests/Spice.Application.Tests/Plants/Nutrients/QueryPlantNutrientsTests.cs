using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Plants.Exceptions;
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

        [TestCase(TestName = "Get all by plant id query on plants throws exception if plant does not exist")]
        public void GetAllByPlantThrowsExceptionIfPlantDoesNotExist()
        {
            // Given
            Guid plantId = Guid.NewGuid();

            // When
            Func<Task<IEnumerable<AdministeredNutrient>>> queryNutrients = async () => await _queries.GetByPlant(plantId);

            // Then
            queryNutrients.Should().Throw<PlantDoesNotExistException>();
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

        [TestCase(TestName = "Get by plant id query on plant nutrients throws exception if plant does not exist")]
        public void GetByPlantThrowsExceptionIfPlantDoesNotExist()
        {
            // Given
            Guid plantId = Guid.NewGuid();
            Guid id = Guid.NewGuid();

            // When
            Func<Task<AdministeredNutrient>> queryForData = async () => await _queries.Get(plantId, id);

            // Then
            queryForData.Should().Throw<PlantDoesNotExistException>();
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

        [TestCase(TestName = "Get summary of administered nutrients by plant id throws exception if plant does not exist")]
        public void SumAdministeredNutrientsThrowExceptionIfPlantDoesNotExist()
        {
            // Given
            Guid plantId = Guid.NewGuid();

            // When
            Func<Task<IEnumerable<AdministeredPlantNutrientsSummaryModel>>> queryForData = async () => await _queries.Sum(plantId);

            // Then
            queryForData.Should().Throw<PlantDoesNotExistException>();
        }

        [TestCase(TestName = "Get summary of administered nutrients by plant id returns administered nutrients summary")]
        public async Task SumAdministeredNutrientsReturnsAdministeredNutrientsSummary()
        {
            // Given
            Plant plant = SeedDatabaseForGetNutrientSummaryTesting();

            // When
            IEnumerable<AdministeredPlantNutrientsSummaryModel> administeredNutrientFromDatabase =
                await _queries.Sum(plant.Id);

            // Then
            administeredNutrientFromDatabase.Should().NotBeNullOrEmpty();
            AdministeredPlantNutrientsSummaryModel waterSummary =
                administeredNutrientFromDatabase.Single(x => x.Nutrient.Name == "Water");
            waterSummary.Nutrient.Should().NotBeNull();
            waterSummary.TotalAmount.Should().Be(2);

            AdministeredPlantNutrientsSummaryModel fertilizerSummary =
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