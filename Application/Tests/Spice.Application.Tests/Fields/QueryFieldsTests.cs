using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spice.Application.Fields;
using Spice.Domain;
using Spice.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Fields
{
    [TestFixture]
    public class QueryFieldsTests
    {
        private QueryFields _queries;
        private SpiceContext _service;

        [SetUp]
        public void SetUp()
        {
            _service = SetupInMemoryDatabase();
            _service.Database.EnsureCreated();
            _queries = new QueryFields(_service);
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