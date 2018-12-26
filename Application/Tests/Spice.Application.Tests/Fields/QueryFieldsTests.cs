using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spice.Application.Fields;
using Spice.Persistence;
using System;
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
            throw new NotImplementedException();
        }

        [TestCase(TestName = "Get by id query on fields returns null if field was not found")]
        public async Task GetFieldReturnsNullWhenNotFound()
        {
            throw new NotImplementedException();
        }

        [TestCase(TestName = "Get by id query on fields returns field if found")]
        public async Task GetFieldReturnsFieldWhenFound()
        {
            throw new NotImplementedException();
        }
    }
}