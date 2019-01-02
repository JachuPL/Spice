using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Spice.Application.Common.Exceptions;
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

        [TestCase(TestName = "GET list of species returns \"OK\" and correct content type")]
        public async Task GetListReturnsSpeciesAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.GetAll()).Returns(A.Fake<IEnumerable<Domain.Species>>());

            // When
            var response = await Client.GetAsync(EndPointFactory.ListEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.GetAll()).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET single species returns \"Not Found\" and correct content type")]
        public async Task GetSpeciesReturnsNotFoundAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).Returns(Task.FromResult<Domain.Species>(null));

            // When
            var response = await Client.GetAsync(EndPointFactory.DetailsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET single species returns \"OK\" and correct content type")]
        public async Task GetSpeciesReturnsSpeciesAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).Returns(A.Fake<Domain.Species>());

            // When
            var response = await Client.GetAsync(EndPointFactory.DetailsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST species returns \"Conflict\" and correct content type if resource state exception occured")]
        public async Task PostNewSpeciesReturnsConflictIfResourceStateExceptionOccured()
        {
            // Given
            A.CallTo(() => _fakeCommand.Create(A<CreateSpeciesModel>.Ignored)).Throws(A.Fake<ResourceStateException>());

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Create(A<CreateSpeciesModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST species returns \"Created\" and sets Location header")]
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

        [TestCase(TestName = "POST species returns \"Bad Request\" and correct content type for incorrect data")]
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

        [TestCase(TestName = "PUT species returns \"Conflict\" and correct content type if resource state exception occured")]
        public async Task PutSpeciesReturnsConflictIfResourceStateExceptionOccured()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<UpdateSpeciesModel>.Ignored)).Throws(A.Fake<ResourceStateException>());

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<UpdateSpeciesModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT species returns \"Not Found\" and correct content type if species does not exist")]
        public async Task PutSpeciesReturnsNotFoundAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<UpdateSpeciesModel>.Ignored))
                .Returns(Task.FromResult<Domain.Species>(null));

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<UpdateSpeciesModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT species returns \"OK\" and correct content type")]
        public async Task PutSpeciesReturnsSpeciesAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<UpdateSpeciesModel>.Ignored)).Returns(A.Fake<Domain.Species>());

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<UpdateSpeciesModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT species returns \"BadRequest\" and correct content type")]
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

        [TestCase(TestName = "DELETE species returns \"No Content\"")]
        public async Task DeleteSpeciesReturnsNoContentAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Delete(A<Guid>.Ignored)).Returns(Task.CompletedTask);

            // When
            var response = await Client.DeleteAsync(EndPointFactory.DeleteEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            A.CallTo(() => _fakeCommand.Delete(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET summary of species nutrition returns \"Not Found\" and correct content type if species does not exist")]
        public async Task GetSummaryOfPlantEventReturnsNotFoundAndCorrectContentTypeIfResourceNotFoundExceptionOccured()
        {
            // Given
            A.CallTo(() => _fakeQuery.Summary(A<Guid>.Ignored, A<DateTime?>.Ignored, A<DateTime?>.Ignored))
                .Returns(Task.FromResult<IEnumerable<SpeciesNutritionSummaryModel>>(null));

            // When
            var response = await Client.GetAsync(EndPointFactory.SpeciesSummaryEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Summary(A<Guid>.Ignored, A<DateTime?>.Ignored, A<DateTime?>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET summary of species nutrition returns \"OK\" and correct content type for dates within range")]
        public async Task GetSummaryOfPlantEventReturnsOKAndCorrectContentType()
        {
            // Given
            DateTime fromDate = new DateTime(2018, 12, 01, 00, 00, 00);
            DateTime toDate = new DateTime(2018, 12, 31, 23, 59, 59);
            A.CallTo(() => _fakeQuery.Summary(A<Guid>.Ignored, fromDate, toDate))
                .Returns(A.Fake<IEnumerable<SpeciesNutritionSummaryModel>>());

            // When
            var response = await Client.GetAsync(EndPointFactory.SpeciesSummaryWithinDateRangeEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Summary(A<Guid>.Ignored, fromDate, toDate))
                .MustHaveHappenedOnceExactly();
        }
    }
}