﻿using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Spice.Application.Fields.Exceptions;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Application.Species.Exceptions;
using Spice.Domain.Plants;
using Spice.ViewModels.Plants;
using Spice.WebAPI.Tests.Common;
using Spice.WebAPI.Tests.Plants.Factories.Plants;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spice.WebAPI.Tests.Plants
{
    [TestFixture]
    internal sealed class PlantsIntegrationTests : AbstractIntegrationTestsBaseFixture
    {
        private IQueryPlants _fakeQuery;
        private ICommandPlants _fakeCommand;

        protected override void ServicesConfiguration(IServiceCollection services)
        {
            _fakeQuery = A.Fake<IQueryPlants>();
            services.AddSingleton(_fakeQuery);

            _fakeCommand = A.Fake<ICommandPlants>();
            services.AddSingleton(_fakeCommand);
        }

        [SetUp]
        public void SetUp()
        {
            Fake.ClearRecordedCalls(_fakeQuery);
            Fake.ClearRecordedCalls(_fakeCommand);
        }

        [TestCase(TestName = "GET list of plants returns \"OK\" and correct content type")]
        public async Task GetListReturnsPlantsAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.GetAll()).Returns(A.Fake<IEnumerable<Plant>>());

            // When
            var response = await Client.GetAsync(EndPointFactory.ListEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.GetAll()).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET single plant returns \"Not Found\" and correct content type")]
        public async Task GetPlantReturnsNotFoundAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).Returns(Task.FromResult<Plant>(null));

            // When
            var response = await Client.GetAsync(EndPointFactory.DetailsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET single plant returns \"OK\" and correct content type")]
        public async Task GetPlantReturnsPlantAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).Returns(A.Fake<Plant>());

            // When
            var response = await Client.GetAsync(EndPointFactory.DetailsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST plant returns \"Conflict\" and correct content type if plant exists at specific coordinates")]
        public async Task PostNewPlantReturnsConflictIfPlantExistsAtSpecificCoordinates()
        {
            // Given
            A.CallTo(() => _fakeCommand.Create(A<CreatePlantModel>.Ignored))
                .Throws(new PlantExistsAtCoordinatesException(0, 0));

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Create(A<CreatePlantModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST plant returns \"Conflict\" and correct content type if field by given id does not exist")]
        public async Task PostNewPlantReturnsConflictIfFieldDoesNotExistById()
        {
            // Given
            A.CallTo(() => _fakeCommand.Create(A<CreatePlantModel>.Ignored))
                .Throws(new FieldDoesNotExistException(Guid.NewGuid()));

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Create(A<CreatePlantModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST plant returns \"Conflict\" and correct content type if species by given id does not exist")]
        public async Task PostNewPlantReturnsConflictIfSpeciesDoesNotExistById()
        {
            // Given
            A.CallTo(() => _fakeCommand.Create(A<CreatePlantModel>.Ignored))
                .Throws(new SpeciesDoesNotExistException(Guid.NewGuid()));

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Create(A<CreatePlantModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST plant returns \"Created\" and sets Location header")]
        public async Task PostNewPlantReturnsCreatedAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Create(A<CreatePlantModel>.Ignored)).Returns(Guid.NewGuid());

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            A.CallTo(() => _fakeCommand.Create(A<CreatePlantModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST plant returns \"Bad Request\" and correct content type for incorrect data")]
        public async Task PostNewPlantReturnsBadRequestAndCorrectContentType()
        {
            // Given
            CreatePlantViewModel model = ViewModelFactory.CreateInvalidCreationModel();

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), model);

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

        [TestCase(TestName = "PUT plant returns \"Conflict\" and correct content type if other plant at specified coordinates already exists")]
        public async Task PutPlantReturnsConflictIfNewCoordinatesConflictWithExistingPlant()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<UpdatePlantModel>.Ignored))
                .Throws(new PlantExistsAtCoordinatesException(0, 0));

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<UpdatePlantModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT plant returns \"Conflict\" and correct content type if field by given id does not exist")]
        public async Task PutPlantReturnsConflictIfFieldDoesNotExistById()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<UpdatePlantModel>.Ignored))
                .Throws(new FieldDoesNotExistException(Guid.NewGuid()));

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<UpdatePlantModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT plant returns \"Conflict\" and correct content type if species by given id does not exist")]
        public async Task PutPlantReturnsConflictIfSpeciesDoesNotExistById()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<UpdatePlantModel>.Ignored))
                .Throws(new SpeciesDoesNotExistException(Guid.NewGuid()));

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<UpdatePlantModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT plant returns \"Not Found\" and correct content type if plant was not found")]
        public async Task PutPlantReturnsNotFoundAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<UpdatePlantModel>.Ignored)).Returns(Task.FromResult<Plant>(null));

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<UpdatePlantModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT plant returns \"OK\" and correct content type")]
        public async Task PutPlantReturnsPlantAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<UpdatePlantModel>.Ignored)).Returns(A.Fake<Plant>());

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<UpdatePlantModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT plant returns \"BadRequest\" and correct content type")]
        public async Task PutPlantReturnsBadRequestAndCorrectContentType()
        {
            // Given
            UpdatePlantViewModel model = ViewModelFactory.CreateInvalidUpdateModel();

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), model);

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

        [TestCase(TestName = "DELETE plant returns \"No Content\"")]
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