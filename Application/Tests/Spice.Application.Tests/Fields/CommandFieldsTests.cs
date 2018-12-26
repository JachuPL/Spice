using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Spice.Application.Fields;
using Spice.Application.Fields.Exceptions;
using Spice.Application.Fields.Models;
using Spice.Application.Tests.Common.Base;
using Spice.AutoMapper;
using Spice.Domain;
using Spice.Persistence;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Tests.Fields
{
    internal sealed class CommandFieldsTests : AbstractInMemoryDatabaseAwareTestFixture
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

        [TestCase(TestName = "Create field throws exception if field with specified name already exists")]
        public void CreateFieldThrowsExceptionOnNameConflict()
        {
            // Given
            Field existingField = ModelFactory.DomainModel();
            Guid existingFieldId = SeedDatabase(existingField);
            CreateFieldModel model = ModelFactory.CreationModel();

            // When
            Func<Task> createField = async () => await _commands.Create(model);

            // Then
            createField.Should().Throw<FieldWithNameAlreadyExistsException>();
        }

        [TestCase(TestName = "Create field returns Guid on success")]
        public async Task CreateFieldReturnsGuidOnSuccess()
        {
            // Given
            CreateFieldModel model = ModelFactory.CreationModel();

            // When
            Guid fieldId = await _commands.Create(model);

            // Then
            fieldId.Should().NotBe(Guid.Empty);
        }

        [TestCase(TestName = "Update field throws exception if field with specified name already exists")]
        public void UpdateFieldThrowsExceptionOnNameConflict()
        {
            // Given
            Field existingField = ModelFactory.DomainModel();
            Guid existingFieldId = SeedDatabase(existingField);
            Field updatedField = ModelFactory.DomainModel("Field B");
            Guid updatedFieldId = SeedDatabase(updatedField);
            UpdateFieldModel model = ModelFactory.UpdateModel(updatedFieldId);

            // When
            Func<Task> updateField = async () => await _commands.Update(model);

            // Then
            updateField.Should().Throw<FieldWithNameAlreadyExistsException>();
        }

        [TestCase(TestName = "Update field throws exception if field does not exist")]
        public void UpdateFieldThrowsExceptionIfFieldDoesNotExist()
        {
            // Given
            UpdateFieldModel model = ModelFactory.UpdateModel(Guid.NewGuid());

            // When
            Func<Task> updateField = async () => await _commands.Update(model);

            // Then
            updateField.Should().Throw<FieldDoesNotExistException>();
        }

        [TestCase(TestName = "Update field returns updated field on success")]
        public async Task UpdateFieldReturnsFieldOnSuccess()
        {
            // Given
            Field field = ModelFactory.DomainModel("Field B", 13, 37);
            Guid fieldId = SeedDatabase(field);
            UpdateFieldModel model = ModelFactory.UpdateModel(fieldId);

            // When
            field = await _commands.Update(model);

            // Then
            field.Should().NotBeNull();
            field.Name.Should().Be("Field A");
            field.Latitude.Should().Be(52);
            field.Longtitude.Should().Be(20);
        }

        [TestCase(TestName = "Delete field succeeds")]
        public async Task DeleteFieldShouldSucceed()
        {
            // Given
            Guid fieldId = SeedDatabase(ModelFactory.DomainModel("Field B", 13, 37));

            // When
            await _commands.Delete(fieldId);

            // Then
            Field field = await _service.Fields.FindAsync(fieldId);
            field.Should().BeNull();
        }
    }
}