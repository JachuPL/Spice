using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spice.Application.Fields;
using Spice.AutoMapper;
using Spice.Persistence;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Fields
{
    public class CommandFieldsTests
    {
        private CommandFields _commands;
        private SpiceContext _service;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _service = SetupInMemoryDatabase();
            _service.Database.EnsureCreated();
            _mapper = AutoMapperFactory.CreateMapper();
            _commands = new CommandFields(_service, _mapper);
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

        [TestCase(TestName = "Create field throws exception if field with specified name already exists")]
        public void CreateFieldThrowsExceptionOnNameConflict()
        {
            throw new NotImplementedException();
        }

        [TestCase(TestName = "Create field returns Guid on success")]
        public async Task CreateFieldReturnsGuidOnSuccess()
        {
            throw new NotImplementedException();
        }

        [TestCase(TestName = "Update field throws exception if field with specified name already exists")]
        public void UpdateFieldThrowsExceptionOnNameConflict()
        {
            throw new NotImplementedException();
        }

        [TestCase(TestName = "Update field returns null if field does not exist")]
        public async Task UpdateFieldReturnsNullIfFieldDoesNotExist()
        {
            throw new NotImplementedException();
        }

        [TestCase(TestName = "Update field returns updated field on success")]
        public async Task UpdateFieldReturnsFieldOnSuccess()
        {
            throw new NotImplementedException();
        }

        [TestCase(TestName = "Delete field succeeds")]
        public async Task DeleteFieldShouldSucceed()
        {
            throw new NotImplementedException();
        }
    }
}