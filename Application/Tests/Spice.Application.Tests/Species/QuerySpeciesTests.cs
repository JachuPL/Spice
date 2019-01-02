using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Species;
using Spice.Application.Species.Models;
using Spice.Application.Tests.Common.Base;
using Spice.Domain;
using Spice.Domain.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _queries = new QuerySpecies(DatabaseContext, Mapper);
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
            IEnumerable<Domain.Species> species = await _queries.GetAll();

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
            Domain.Species species = await _queries.Get(speciesId);

            // Then
            species.Should().BeNull();
        }

        [TestCase(TestName = "Get by id query on species returns species if found")]
        public async Task GetSpeciesReturnsSpeciesWhenFound()
        {
            // Given
            Guid SpeciesId = SeedDatabaseForGetByIdTesting();

            // When
            Domain.Species species = await _queries.Get(SpeciesId);

            // Then
            species.Should().NotBeNull();
        }

        private Guid SeedDatabaseForGetByIdTesting()
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                Domain.Species species = ModelFactory.DomainModel();
                ctx.Species.Add(species);
                ctx.Save();
                return species.Id;
            }
        }

        [TestCase(TestName = "Get summary of species nutrition returns null if species does not exist")]
        public async Task SpeciesSummaryReturnsNullIfSpeciesDoesNotExist()
        {
            // Given
            Guid plantId = Guid.NewGuid();

            // When
            IEnumerable<SpeciesNutritionSummaryModel> nutritionSummary = await _queries.Summary(plantId);

            // Then
            nutritionSummary.Should().BeNull();
        }

        [TestCase(TestName = "Get summary of species nutrition returns nutrition summary from entire history")]
        public async Task SpeciesSummaryReturnsSpeciesSummaryFromEntireHistory()
        {
            // Given
            Domain.Species species = SeedDatabaseForGetSpeciesSummaryTesting();

            // When
            IEnumerable<SpeciesNutritionSummaryModel> speciesFromDatabase = await _queries.Summary(species.Id);

            // Then
            speciesFromDatabase.Should().NotBeNullOrEmpty();
            speciesFromDatabase.Single(x => x.Nutrient.Name == "Water").TotalAmount.Should().Be(5);
            speciesFromDatabase.Single(x => x.Nutrient.Name == "Fertilizer").TotalAmount.Should().Be(7);
        }

        [TestCase(TestName = "Get summary of species nutrition returns nutrition summary from specified date range")]
        public async Task SpeciesSummaryReturnsSpeciesSummaryFromSpecifiedDateRange()
        {
            // Given
            Domain.Species species = SeedDatabaseForGetSpeciesSummaryTesting();

            // When
            IEnumerable<SpeciesNutritionSummaryModel> speciesFromDatabase = await _queries.Summary(species.Id,
                new DateTime(2018, 1, 1, 0, 0, 0),
                new DateTime(2018, 12, 31, 23, 59, 59));

            // Then
            speciesFromDatabase.Should().NotBeNullOrEmpty();
            speciesFromDatabase.Single(x => x.Nutrient.Name == "Water").TotalAmount.Should().Be(2);
            speciesFromDatabase.Single(x => x.Nutrient.Name == "Fertilizer").TotalAmount.Should().Be(4);
        }

        private Domain.Species SeedDatabaseForGetSpeciesSummaryTesting()
        {
            // test data overview: three plants of one species growing on three different fields
            Domain.Species species = ModelFactory.DomainModel();
            Plant plant1 = Plants.ModelFactory.DomainModel(species: species);
            Plant plant2 = Plants.ModelFactory.DomainModel(species: species);
            Plant plant3 = Plants.ModelFactory.DomainModel(species: species);

            // two nutrients - water and fertilizer
            Nutrient water = Nutrients.ModelFactory.DomainModel("Water");
            Nutrient fertilizer = Nutrients.ModelFactory.DomainModel("Fertilizer", dosageUnits: "g");

            // plant #1 was watered 3 times with 1ml of water and fertilized once with 1 gram of fertilizer
            AdministeredNutrient waterPlant1_1 = Plants.Nutrients.ModelFactory.DomainModel(water, plant1, new DateTime(2017, 01, 01, 0, 0, 0));
            AdministeredNutrient waterPlant1_2 = Plants.Nutrients.ModelFactory.DomainModel(water, plant1, new DateTime(2018, 01, 01, 0, 0, 0));
            AdministeredNutrient waterPlant1_3 = Plants.Nutrients.ModelFactory.DomainModel(water, plant1, new DateTime(2019, 01, 01, 0, 0, 0));
            AdministeredNutrient fertilizerPlant1 = Plants.Nutrients.ModelFactory.DomainModel(fertilizer, plant1, new DateTime(2018, 02, 01, 0, 0, 0));
            plant1.AdministeredNutrients.Add(waterPlant1_1);
            plant1.AdministeredNutrients.Add(waterPlant1_2);
            plant1.AdministeredNutrients.Add(waterPlant1_3);
            plant1.AdministeredNutrients.Add(fertilizerPlant1);

            // plant #2 was watered 2 times with 1ml of water and fertilized twice with 1 gram of fertilizer
            AdministeredNutrient waterPlant2_1 = Plants.Nutrients.ModelFactory.DomainModel(water, plant2, new DateTime(2017, 01, 01, 0, 0, 0));
            AdministeredNutrient waterPlant2_2 = Plants.Nutrients.ModelFactory.DomainModel(water, plant2, new DateTime(2018, 01, 01, 0, 0, 0));
            AdministeredNutrient fertilizerPlant2_1 = Plants.Nutrients.ModelFactory.DomainModel(fertilizer, plant2, new DateTime(2018, 02, 01, 0, 0, 0));
            AdministeredNutrient fertilizerPlant2_2 = Plants.Nutrients.ModelFactory.DomainModel(fertilizer, plant2, new DateTime(2019, 01, 01, 0, 0, 0));
            plant2.AdministeredNutrients.Add(waterPlant2_1);
            plant2.AdministeredNutrients.Add(waterPlant2_2);
            plant2.AdministeredNutrients.Add(fertilizerPlant2_1);
            plant2.AdministeredNutrients.Add(fertilizerPlant2_2);

            // plant #3 was not watered and was fertilized four times with 1 gram of fertilizer
            AdministeredNutrient fertilizerPlant3_1 = Plants.Nutrients.ModelFactory.DomainModel(fertilizer, plant3, new DateTime(2017, 01, 01, 0, 0, 0));
            AdministeredNutrient fertilizerPlant3_2 = Plants.Nutrients.ModelFactory.DomainModel(fertilizer, plant3, new DateTime(2018, 01, 01, 0, 0, 0));
            AdministeredNutrient fertilizerPlant3_3 = Plants.Nutrients.ModelFactory.DomainModel(fertilizer, plant3, new DateTime(2018, 02, 01, 0, 0, 0));
            AdministeredNutrient fertilizerPlant3_4 = Plants.Nutrients.ModelFactory.DomainModel(fertilizer, plant3, new DateTime(2019, 01, 01, 0, 0, 0));
            plant3.AdministeredNutrients.Add(fertilizerPlant3_1);
            plant3.AdministeredNutrients.Add(fertilizerPlant3_2);
            plant3.AdministeredNutrients.Add(fertilizerPlant3_3);
            plant3.AdministeredNutrients.Add(fertilizerPlant3_4);

            // Water used in 2017: 2
            // Water used in 2018: 2
            // Water used in 2019: 1
            // Fertilizer used in 2017: 1
            // Fertilizer used in 2018: 4
            // Fertilizer used in 2019: 2
            // Total used water: 5 ml
            // Total used fertilizer: 7 g
            DatabaseContext.Species.Add(species);
            DatabaseContext.Nutrients.Add(water);
            DatabaseContext.Nutrients.Add(fertilizer);
            DatabaseContext.Plants.Add(plant1);
            DatabaseContext.Plants.Add(plant2);
            DatabaseContext.Plants.Add(plant3);
            DatabaseContext.Save();

            return species;
        }
    }
}