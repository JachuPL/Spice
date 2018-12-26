using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Fields;
using Spice.Application.Tests.Common.Base;
using Spice.Domain;
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
            using (var ctx = SetupInMemoryDatabase())
            {
                ctx.Fields.Add(ModelFactory.DomainModel("Field A"));
                ctx.Fields.Add(ModelFactory.DomainModel("Field B"));
                ctx.Fields.Add(ModelFactory.DomainModel("Field C"));
                ctx.Fields.Add(ModelFactory.DomainModel("Field D"));
                ctx.Save();
            }
        }

        [TestCase(TestName = "Get by id query on fields returns null if field was not found")]
        public async Task GetFieldReturnsNullWhenNotFound()
        {
            // When
            Field field = await _queries.Get(Guid.NewGuid());

            // Then
            field.Should().BeNull();
        }

        [TestCase(TestName = "Get by id query on fields returns field if found")]
        public async Task GetFieldReturnsFieldWhenFound()
        {
            // Given
            Guid fieldId = SeedDatabaseForGetByIdTesting();

            // When
            Field field = await _queries.Get(fieldId);

            // Then
            field.Should().NotBeNull();
        }

        private Guid SeedDatabaseForGetByIdTesting()
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                Field field = ModelFactory.DomainModel();
                ctx.Fields.Add(field);
                ctx.Save();
                return field.Id;
            }
        }
    }
}