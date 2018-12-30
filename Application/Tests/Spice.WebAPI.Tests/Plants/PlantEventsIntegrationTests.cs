using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Spice.Application.Plants.Events.Exceptions;
using Spice.Application.Plants.Events.Interfaces;
using Spice.Application.Plants.Events.Models;
using Spice.Application.Plants.Exceptions;
using Spice.Domain.Plants.Events;
using Spice.ViewModels.Plants.Events;
using Spice.WebAPI.Tests.Common;
using Spice.WebAPI.Tests.Plants.Factories.Events;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spice.WebAPI.Tests.Plants
{
    [TestFixture]
    internal sealed class PlantEventsIntegrationTests : AbstractIntegrationTestsBaseFixture
    {
        private IQueryPlantEvents _fakeQuery;
        private ICommandPlantEvents _fakeCommand;

        protected override void ServicesConfiguration(IServiceCollection services)
        {
            _fakeQuery = A.Fake<IQueryPlantEvents>();
            services.AddSingleton(_fakeQuery);

            _fakeCommand = A.Fake<ICommandPlantEvents>();
            services.AddSingleton(_fakeCommand);
        }

        [SetUp]
        public void SetUp()
        {
            Fake.ClearRecordedCalls(_fakeQuery);
            Fake.ClearRecordedCalls(_fakeCommand);
        }

        [TestCase(TestName = "GET list of plant events returns \"Not Found\" and correct content type if plant does not exist")]
        public async Task GetListReturnsNotFoundIfPlantDoesNotExist()
        {
            // Given
            A.CallTo(() => _fakeQuery.GetByPlant(A<Guid>.Ignored)).Returns(Task.FromResult<IEnumerable<Event>>(null));

            // When
            var response = await Client.GetAsync(EndPointFactory.ListEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeQuery.GetByPlant(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET list of plant events returns \"OK\" and correct content type")]
        public async Task GetListReturnsPlantEventssAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.GetByPlant(A<Guid>.Ignored)).Returns(A.Fake<IEnumerable<Event>>());

            // When
            var response = await Client.GetAsync(EndPointFactory.ListEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.GetByPlant(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET single plant event returns \"Not Found\" and correct content type if plant does not exist")]
        public async Task GetPlantEventReturnsNotFoundAndCorrectContentTypeIfPlantDoesNotExist()
        {
            // Given
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored, A<Guid>.Ignored)).Returns(Task.FromResult<Event>(null));

            // When
            var response = await Client.GetAsync(EndPointFactory.DetailsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored, A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET single plant event returns \"Not Found\" and correct content type if plant event does not exist")]
        public async Task GetPlantEventReturnsNotFoundAndCorrectContentTypeIfPlantEventDoesNotExist()
        {
            // Given
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored, A<Guid>.Ignored)).Returns(Task.FromResult<Event>(null));

            // When
            var response = await Client.GetAsync(EndPointFactory.DetailsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored, A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET single plant event returns \"OK\" and correct content type")]
        public async Task GetPlantEventReturnsPlantEventAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored, A<Guid>.Ignored)).Returns(A.Fake<Event>());

            // When
            var response = await Client.GetAsync(EndPointFactory.DetailsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored, A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST plant event returns \"Conflict\" and correct content type if plant by id does not exist")]
        public async Task PostNewPlantEventReturnsConflictIfPlantDoesNotExist()
        {
            // Given
            A.CallTo(() => _fakeCommand.Create(A<Guid>.Ignored, A<CreatePlantEventModel>.Ignored))
                .Throws(new PlantDoesNotExistException(Guid.NewGuid()));

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Create(A<Guid>.Ignored, A<CreatePlantEventModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST plant event returns \"Conflict\" and correct content type if occurence date is earlier than planting date or in the future")]
        public async Task PostNewPlantEventReturnsConflictIfOccurenceDateIsEarlierThanPlantingDateOrInTheFuture()
        {
            // Given
            A.CallTo(() => _fakeCommand.Create(A<Guid>.Ignored, A<CreatePlantEventModel>.Ignored))
                .Throws(new EventOccurenceDateBeforePlantDateOrInTheFutureException());

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Create(A<Guid>.Ignored, A<CreatePlantEventModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST plant event returns \"Created\" and sets Location header")]
        public async Task PostNewPlantEventReturnsCreatedAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Create(A<Guid>.Ignored, A<CreatePlantEventModel>.Ignored)).Returns(Guid.NewGuid());

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            A.CallTo(() => _fakeCommand.Create(A<Guid>.Ignored, A<CreatePlantEventModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST plant event returns \"Bad Request\" and correct content type for incorrect data")]
        public async Task PostNewPlantEventReturnsBadRequestAndCorrectContentType()
        {
            // Given
            CreatePlantEventViewModel model = ViewModelFactory.CreateInvalidCreationModel();

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), model);

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

        [TestCase(TestName = "PUT plant event returns \"Conflict\" and correct content type if plant by given id does not exist")]
        public async Task PutPlantEventReturnsConflictIfPlantDoesNotExistById()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdatePlantEventModel>.Ignored))
                .Throws(new PlantDoesNotExistException(Guid.NewGuid()));

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdatePlantEventModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT plant event returns \"Conflict\" and correct content type if occurence date is earlier than planting date or in the future")]
        public async Task PutPlantEventReturnsConflictIfOccurenceDateIsEarlierThanPlantingDateOrInTheFuture()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdatePlantEventModel>.Ignored))
                .Throws(new EventOccurenceDateBeforePlantDateOrInTheFutureException());

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdatePlantEventModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT plant event returns \"Not Found\" and correct content type if plant event was not found")]
        public async Task PutPlantEventReturnsNotFoundAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdatePlantEventModel>.Ignored)).Returns(Task.FromResult<Event>(null));

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdatePlantEventModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT  plant event returns \"OK\" and correct content type")]
        public async Task PutPlantEventReturnsPlantEventAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdatePlantEventModel>.Ignored)).Returns(A.Fake<Event>());

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdatePlantEventModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT  plant event returns \"Bad Request\" and correct content type")]
        public async Task PutPlantEventReturnsBadRequestAndCorrectContentType()
        {
            // Given
            UpdatePlantEventViewModel model = ViewModelFactory.CreateInvalidUpdateModel();

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), model);

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

        [TestCase(TestName = "DELETE  plant event returns \"No Content\"")]
        public async Task DeletePlantReturnsNoContentAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Delete(A<Guid>.Ignored, A<Guid>.Ignored)).Returns(Task.CompletedTask);

            // When
            var response = await Client.DeleteAsync(EndPointFactory.DeleteEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            A.CallTo(() => _fakeCommand.Delete(A<Guid>.Ignored, A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET sum of plant events returns \"Not Found\" and correct content type if plant by id was not found")]
        public async Task GetSumOfPlantEventReturnsNotFoundAndCorrectContentTypeIfPlantDoesNotExist()
        {
            // Given
            A.CallTo(() => _fakeQuery.Sum(A<Guid>.Ignored)).Throws(new PlantDoesNotExistException(Guid.NewGuid()));

            // When
            var response = await Client.GetAsync(EndPointFactory.SumTotalEventsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Sum(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET sum of plant events returns \"OK\" and correct content type")]
        public async Task GetSumOfPlantEventReturnsOKAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.Sum(A<Guid>.Ignored)).Returns(A.Fake<IEnumerable<OccuredPlantEventsSummaryModel>>());

            // When
            var response = await Client.GetAsync(EndPointFactory.SumTotalEventsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Sum(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}