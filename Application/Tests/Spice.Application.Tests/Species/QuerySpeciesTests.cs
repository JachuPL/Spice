using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Species;
using Spice.Application.Species.Models;
using Spice.Application.Tests.Common.Base;
using Spice.Domain;
using Spice.Domain.Plants;
using Spice.Persistence;
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
            using (SpiceContext ctx = SetupInMemoryDatabase())
            {
                ctx.Species.Add(ModelFactory.DomainModel("Species A"));
                ctx.Species.Add(ModelFactory.DomainModel("Species B"));
                ctx.Species.Add(ModelFactory.DomainModel("Species C"));
                ctx.Species.Add(ModelFactory.DomainModel("Species D"));
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
            Guid speciesId = SeedDatabaseForGetByIdTesting();

            // When
            Domain.Species species = await _queries.Get(speciesId);

            // Then
            species.Should().NotBeNull();
        }

        private Guid SeedDatabaseForGetByIdTesting()
        {
            using (SpiceContext ctx = SetupInMemoryDatabase())
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
            SpeciesNutritionSummaryModel waterSummary = speciesFromDatabase.Single(x => x.Nutrient.Name == "Water");
            waterSummary.Nutrient.Should().NotBeNull();
            waterSummary.TotalAmount.Should().Be(2);
            waterSummary.FirstAdministration.Should().Be(new DateTime(2018, 01, 01, 0, 0, 0));
            waterSummary.LastAdministration.Should().Be(new DateTime(2018, 01, 02, 0, 0, 0));
            SpeciesNutritionSummaryModel fertilizerSummary = speciesFromDatabase.Single(x => x.Nutrient.Name == "Fertilizer");
            fertilizerSummary.Nutrient.Should().NotBeNull();
            fertilizerSummary.TotalAmount.Should().Be(4);
            fertilizerSummary.FirstAdministration.Should().Be(new DateTime(2018, 01, 01, 0, 0, 0));
            fertilizerSummary.LastAdministration.Should().Be(new DateTime(2018, 02, 02, 0, 0, 0));
        }

        private Domain.Species SeedDatabaseForGetSpeciesSummaryTesting()
        {
            using (SpiceContext ctx = SetupInMemoryDatabase())
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
                AdministeredNutrient waterPlant11 =
                    Plants.Nutrients.ModelFactory.DomainModel(water, plant1, new DateTime(2017, 01, 01, 0, 0, 0));
                AdministeredNutrient waterPlant12 =
                    Plants.Nutrients.ModelFactory.DomainModel(water, plant1, new DateTime(2018, 01, 01, 0, 0, 0));
                AdministeredNutrient waterPlant13 =
                    Plants.Nutrients.ModelFactory.DomainModel(water, plant1, new DateTime(2019, 01, 01, 0, 0, 0));
                AdministeredNutrient fertilizerPlant1 =
                    Plants.Nutrients.ModelFactory.DomainModel(fertilizer, plant1, new DateTime(2018, 01, 01, 0, 0, 0));
                plant1.AdministeredNutrients.Add(waterPlant11);
                plant1.AdministeredNutrients.Add(waterPlant12);
                plant1.AdministeredNutrients.Add(waterPlant13);
                plant1.AdministeredNutrients.Add(fertilizerPlant1);

                // plant #2 was watered 2 times with 1ml of water and fertilized twice with 1 gram of fertilizer
                AdministeredNutrient waterPlant21 =
                    Plants.Nutrients.ModelFactory.DomainModel(water, plant2, new DateTime(2017, 01, 01, 0, 0, 0));
                AdministeredNutrient waterPlant22 =
                    Plants.Nutrients.ModelFactory.DomainModel(water, plant2, new DateTime(2018, 01, 02, 0, 0, 0));
                AdministeredNutrient fertilizerPlant21 =
                    Plants.Nutrients.ModelFactory.DomainModel(fertilizer, plant2, new DateTime(2018, 02, 01, 0, 0, 0));
                AdministeredNutrient fertilizerPlant22 =
                    Plants.Nutrients.ModelFactory.DomainModel(fertilizer, plant2, new DateTime(2019, 01, 01, 0, 0, 0));
                plant2.AdministeredNutrients.Add(waterPlant21);
                plant2.AdministeredNutrients.Add(waterPlant22);
                plant2.AdministeredNutrients.Add(fertilizerPlant21);
                plant2.AdministeredNutrients.Add(fertilizerPlant22);

                // plant #3 was not watered and was fertilized four times with 1 gram of fertilizer
                AdministeredNutrient fertilizerPlant31 =
                    Plants.Nutrients.ModelFactory.DomainModel(fertilizer, plant3, new DateTime(2017, 01, 01, 0, 0, 0));
                AdministeredNutrient fertilizerPlant32 =
                    Plants.Nutrients.ModelFactory.DomainModel(fertilizer, plant3, new DateTime(2018, 01, 01, 0, 0, 0));
                AdministeredNutrient fertilizerPlant33 =
                    Plants.Nutrients.ModelFactory.DomainModel(fertilizer, plant3, new DateTime(2018, 02, 02, 0, 0, 0));
                AdministeredNutrient fertilizerPlant34 =
                    Plants.Nutrients.ModelFactory.DomainModel(fertilizer, plant3, new DateTime(2019, 01, 01, 0, 0, 0));
                plant3.AdministeredNutrients.Add(fertilizerPlant31);
                plant3.AdministeredNutrients.Add(fertilizerPlant32);
                plant3.AdministeredNutrients.Add(fertilizerPlant33);
                plant3.AdministeredNutrients.Add(fertilizerPlant34);

                // Water used in 2017: 2
                // Water used in 2018: 2
                // Water used in 2019: 1
                // Fertilizer used in 2017: 1
                // Fertilizer used in 2018: 4
                // Fertilizer used in 2019: 2
                // Total used water: 5 ml
                // Total used fertilizer: 7 g
                ctx.Species.Add(species);
                ctx.Nutrients.Add(water);
                ctx.Nutrients.Add(fertilizer);
                ctx.Plants.Add(plant1);
                ctx.Plants.Add(plant2);
                ctx.Plants.Add(plant3);
                ctx.Save();

                return species;
            }
        }
    }
}