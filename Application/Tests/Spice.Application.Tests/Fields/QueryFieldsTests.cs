using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Fields;
using Spice.Application.Tests.Common.Base;
using Spice.Domain;
using Spice.Domain.Plants;
using Spice.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Fields
{
    [TestFixture]
    internal sealed class QueryFieldsTests : AbstractInMemoryDatabaseAwareTestFixture
    {
        private QueryFields _queries;

        [SetUp]
        public void SetUp()
        {
            DatabaseContext = SetupInMemoryDatabase();
            DatabaseContext.Database.EnsureCreated();
            _queries = new QueryFields(DatabaseContext);
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseContext.Database.EnsureDeleted();
        }

        [TestCase(TestName = "GetAll query on fields returns all fields")]
        public async Task GetAllReturnsAllFields()
        {
            // Given
            SeedDatabaseForGetAllTesting();

            // When
            IEnumerable<Field> fields = await _queries.GetAll();

            // Then
            fields.Should().NotBeNullOrEmpty();
        }

        private void SeedDatabaseForGetAllTesting()
        {
            using (SpiceContext ctx = SetupInMemoryDatabase())
            {
                ctx.Fields.Add(ModelFactory.DomainModel("Field A"));
                ctx.Fields.Add(ModelFactory.DomainModel("Field B"));
                ctx.Fields.Add(ModelFactory.DomainModel("Field C"));
                ctx.Fields.Add(ModelFactory.DomainModel("Field D"));
                ctx.Save();
            }
        }

        [TestCase(TestName = "Get by id query on fields returns null if field does not exist")]
        public async Task GetFieldReturnsNullWhenNotFound()
        {
            // Given
            Guid fieldId = Guid.NewGuid();

            // When
            Field field = await _queries.Get(fieldId);

            // Then
            field.Should().BeNull();
        }

        [TestCase(TestName = "Get by id query on fields returns field and plants growing there")]
        public async Task GetFieldReturnsFieldWhenFound()
        {
            // Given
            Guid fieldId = SeedDatabaseForGetByIdTesting();

            // When
            Field field = await _queries.Get(fieldId);

            // Then
            field.Should().NotBeNull();
            field.Plants.Should().NotBeNullOrEmpty();
        }

        private Guid SeedDatabaseForGetByIdTesting()
        {
            using (SpiceContext ctx = SetupInMemoryDatabase())
            {
                Field field = ModelFactory.DomainModel();
                Plant plant = Plants.ModelFactory.DomainModel(field);
                ctx.Fields.Add(field);
                ctx.Plants.Add(plant);
                ctx.Save();
                return field.Id;
            }
        }
    }
}