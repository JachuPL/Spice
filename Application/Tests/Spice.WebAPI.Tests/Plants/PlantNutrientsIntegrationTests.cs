using FakeItEasy;
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
using Spice.WebAPI.Tests.Plants.Factories.Nutrients;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spice.WebAPI.Tests.Plants
{
    [TestFixture]
    internal sealed class PlantNutrientsIntegrationTests : AbstractIntegrationTestsBaseFixture
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

        [TestCase(TestName = "GET list of plant nutrients returns \"OK\" and correct content type")]
        public async Task GetListReturnsPlantNutrientssAndCorrectContentType()
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

        [TestCase(TestName = "GET single administered plant nutrient returns \"Not Found\" and correct content type if plant does not exist")]
        public async Task GetAdministeredPlantNutrientReturnsNotFoundAndCorrectContentTypeIfPlantDoesNotExist()
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

        [TestCase(TestName = "GET single administered plant nutrient returns \"Not Found\" and correct content type if administered plant nutrient does not exist")]
        public async Task GetAdministeredPlantNutrientReturnsNotFoundAndCorrectContentTypeIfAdministeredPlantNutrientDoesNotExist()
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

        [TestCase(TestName = "GET single administered plant nutrient returns \"OK\" and correct content type")]
        public async Task GetAdministeredPlantNutrientReturnsAdministeredPlantNutrientAndCorrectContentType()
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

        [TestCase(TestName = "POST administered plant nutrient returns \"Conflict\" and correct content type if plant by id does not exist")]
        public async Task PostNewAdministeredPlantNutrientReturnsConflictIfPlantDoesNotExist()
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

        [TestCase(TestName = "POST administered plant nutrient returns \"Conflict\" and correct content type if nutrient by id does not exist")]
        public async Task PostNewAdministeredPlantNutrientReturnsConflictIfNutrientDoesNotExist()
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

        [TestCase(TestName = "POST administered plant nutrient returns \"Conflict\" and correct content type if administration date is earlier than planting date")]
        public async Task PostNewAdministeredPlantNutrientReturnsConflictIfAdministrationDateIsEarlierThanPlantingDate()
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

        [TestCase(TestName = "POST administered plant nutrient returns \"Created\" and sets Location header")]
        public async Task PostNewAdministeredPlantNutrientReturnsCreatedAndCorrectContentType()
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

        [TestCase(TestName = "POST administered plant nutrient returns \"Bad Request\" and correct content type for incorrect data")]
        public async Task PostNewAdministeredPlantNutrientReturnsBadRequestAndCorrectContentType()
        {
            // Given
            CreatePlantViewModel model = ViewModelFactory.CreateInvalidCreationModel();

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), model);

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

        [TestCase(TestName = "PUT administered plant nutrient returns \"Conflict\" and correct content type if plant by given id does not exist")]
        public async Task PutAdministeredPlantNutrientReturnsConflictIfPlantDoesNotExistById()
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

        [TestCase(TestName = "PUT administered plant nutrient returns \"Conflict\" and correct content type if nutrient by given id does not exist")]
        public async Task PutAdministeredPlantNutrientReturnsConflictIfNutrientDoesNotExistById()
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

        [TestCase(TestName = "PUT administered plant nutrient returns \"Not Found\" and correct content type if administered plant nutrient was not found")]
        public async Task PutAdministeredPlantNutrientReturnsNotFoundAndCorrectContentType()
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

        [TestCase(TestName = "PUT administered plant nutrient returns \"OK\" and correct content type")]
        public async Task PutAdministeredPlantNutrientReturnsAdministeredPlantNutrientAndCorrectContentType()
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

        [TestCase(TestName = "PUT administered plant nutrient returns \"BadRequest\" and correct content type")]
        public async Task PutAdministeredPlantNutrientReturnsBadRequestAndCorrectContentType()
        {
            // Given
            UpdatePlantViewModel model = ViewModelFactory.CreateInvalidUpdateModel();

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), model);

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

        [TestCase(TestName = "DELETE administered plant nutrient returns \"No Content\"")]
        public async Task DeleteAdministeredPlantReturnsNoContentAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Delete(A<Guid>.Ignored)).Returns(Task.CompletedTask);

            // When
            var response = await Client.DeleteAsync(EndPointFactory.DeleteEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            A.CallTo(() => _fakeCommand.Delete(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET sum of administered plant nutrients returns \"Not Found\" and correct content type if plant by id was not found")]
        public async Task GetSumOfAdministeredPlantNutrientReturnsNotFoundAndCorrectContentTypeIfPlantDoesNotExist()
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

        [TestCase(TestName = "GET sum of administered plant nutrients returns \"OK\" and correct content type")]
        public async Task GetSumOfAdministeredPlantNutrientReturnsOKAndCorrectContentType()
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
    }
}