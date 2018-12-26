using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Spice.Application.Species.Exceptions;
using Spice.Application.Species.Interfaces;
using Spice.Application.Species.Models;
using Spice.ViewModels.Species;
using Spice.WebAPI.Tests.Common;
using Spice.WebAPI.Tests.Species.Factories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spice.WebAPI.Tests.Species
{
    [TestFixture]
    internal sealed class IntegrationTests : AbstractIntegrationTestsBaseFixture
    {
        private IQuerySpecies _fakeQuery;
        private ICommandSpecies _fakeCommand;

        protected override void ServicesConfiguration(IServiceCollection services)
        {
            _fakeQuery = A.Fake<IQuerySpecies>();
            services.AddSingleton(_fakeQuery);

            _fakeCommand = A.Fake<ICommandSpecies>();
            services.AddSingleton(_fakeCommand);
        }

        [SetUp]
        public void SetUp()
        {
            Fake.ClearRecordedCalls(_fakeQuery);
            Fake.ClearRecordedCalls(_fakeCommand);
        }

        [TestCase(TestName = "GET list of Species returns \"OK\" and correct content type")]
        public async Task GetListReturnsSpeciesAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.GetAll()).Returns(A.Fake<IEnumerable<Domain.Plants.Species>>());

            // When
            var response = await Client.GetAsync(EndPointFactory.ListEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.GetAll()).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET single Species returns \"Not Found\" and correct content type")]
        public async Task GetSpeciesReturnsNotFoundAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).Returns(Task.FromResult<Domain.Plants.Species>(null));

            // When
            var response = await Client.GetAsync(EndPointFactory.DetailsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET single Species returns \"OK\" and correct content type")]
        public async Task GetSpeciesReturnsSpeciesAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).Returns(A.Fake<Domain.Plants.Species>());

            // When
            var response = await Client.GetAsync(EndPointFactory.DetailsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST Species returns \"Conflict\" and correct content type if Species with specified name or latin name exists")]
        public async Task PostNewSpeciesReturnsConflictIfSpeciesExistsWithSpecifiedNameOrLatinName()
        {
            // Given
            A.CallTo(() => _fakeCommand.Create(A<CreateSpeciesModel>.Ignored))
                .Throws(new SpeciesWithNameAlreadyExistsException("Species A"));

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Create(A<CreateSpeciesModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST Species returns \"Created\" and sets Location header")]
        public async Task PostNewSpeciesReturnsCreatedAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Create(A<CreateSpeciesModel>.Ignored)).Returns(Guid.NewGuid());

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            A.CallTo(() => _fakeCommand.Create(A<CreateSpeciesModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST Species returns \"Bad Request\" and correct content type for incorrect data")]
        public async Task PostNewSpeciesReturnsBadRequestAndCorrectContentType()
        {
            // Given
            CreateSpeciesViewModel model = ViewModelFactory.CreateInvalidCreationModel();

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), model);

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

        [TestCase(TestName = "PUT Species returns \"Conflict\" and correct content type if other Species with specified name or latin name already exists")]
        public async Task PutSpeciesReturnsConflictIfNewNameConflictsWithExistingSpeciesNameOrLatinName()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<UpdateSpeciesModel>.Ignored))
                .Throws(new SpeciesWithNameAlreadyExistsException("Species A"));

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<UpdateSpeciesModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT Species returns \"Not Found\" and correct content type if Species was not found")]
        public async Task PutSpeciesReturnsNotFoundAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<UpdateSpeciesModel>.Ignored)).Throws(new SpeciesDoesNotExistException(Guid.NewGuid()));

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<UpdateSpeciesModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT Species returns \"OK\" and correct content type")]
        public async Task PutSpeciesReturnsSpeciesAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<UpdateSpeciesModel>.Ignored)).Returns(A.Fake<Domain.Plants.Species>());

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<UpdateSpeciesModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT Species returns \"BadRequest\" and correct content type")]
        public async Task PutSpeciesReturnsBadRequestAndCorrectContentType()
        {
            // Given
            UpdateSpeciesViewModel model = ViewModelFactory.CreateInvalidUpdateModel();

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), model);

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

        [TestCase(TestName = "DELETE Species returns \"No Content\"")]
        public async Task DeletePlantReturnsNoContentAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Delete(A<Guid>.Ignored)).Returns(Task.CompletedTask);

            // When
            var response = await Client.DeleteAsync(EndPointFactory.DeleteEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            A.CallTo(() => _fakeCommand.Delete(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}