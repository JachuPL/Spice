using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Spice.Application.Nutrients.Exceptions;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Nutrients.Exceptions;
using Spice.Application.Plants.Nutrients.Interfaces;
using Spice.Application.Plants.Nutrients.Models;
using Spice.Domain.Plants;
using Spice.ViewModels.Plants.Nutrients;
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
        private IQueryPlantNutrients _fakeQuery;
        private ICommandPlantNutrients _fakeCommand;

        protected override void ServicesConfiguration(IServiceCollection services)
        {
            _fakeQuery = A.Fake<IQueryPlantNutrients>();
            services.AddSingleton(_fakeQuery);

            _fakeCommand = A.Fake<ICommandPlantNutrients>();
            services.AddSingleton(_fakeCommand);
        }

        [SetUp]
        public void SetUp()
        {
            Fake.ClearRecordedCalls(_fakeQuery);
            Fake.ClearRecordedCalls(_fakeCommand);
        }

        [TestCase(TestName = "GET list of plant nutrients returns \"Not Found\" and correct content type if plant does not exist")]
        public async Task GetListReturnsNotFoundAndCorrectContentTypeIfPlantDoesNotExist()
        {
            // Given
            A.CallTo(() => _fakeQuery.GetByPlant(A<Guid>.Ignored))
                .Returns(Task.FromResult<IEnumerable<AdministeredNutrient>>(null));

            // When
            var response = await Client.GetAsync(EndPointFactory.ListEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeQuery.GetByPlant(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET list of plant nutrients returns \"OK\" and correct content type")]
        public async Task GetListReturnsPlantNutrientsAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.GetByPlant(A<Guid>.Ignored)).Returns(A.Fake<IEnumerable<AdministeredNutrient>>());

            // When
            var response = await Client.GetAsync(EndPointFactory.ListEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.GetByPlant(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET administered plant nutrient returns \"Not Found\" and correct content type if plant does not exist")]
        public async Task GetAdministeredPlantNutrientReturnsNotFoundAndCorrectContentTypeIfPlantDoesNotExist()
        {
            // Given
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored, A<Guid>.Ignored))
                .Returns(Task.FromResult<AdministeredNutrient>(null));

            // When
            var response = await Client.GetAsync(EndPointFactory.DetailsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored, A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET administered plant nutrient returns \"Not Found\" and correct content type if nutrition info does not exist")]
        public async Task GetAdministeredPlantNutrientReturnsNotFoundAndCorrectContentTypeIfNutritionInfoDoesNotExist()
        {
            // Given
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored, A<Guid>.Ignored))
                .Returns(Task.FromResult<AdministeredNutrient>(null));

            // When
            var response = await Client.GetAsync(EndPointFactory.DetailsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored, A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET administered plant nutrient returns \"OK\" and correct content type")]
        public async Task GetAdministeredPlantNutrientReturnsAdministeredPlantNutrientAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored, A<Guid>.Ignored)).Returns(A.Fake<AdministeredNutrient>());

            // When
            var response = await Client.GetAsync(EndPointFactory.DetailsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored, A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST administered plant nutrient returns \"Conflict\" and correct content type if plant does not exist")]
        public async Task PostNewAdministeredPlantNutrientReturnsConflictIfPlantDoesNotExist()
        {
            // Given
            A.CallTo(() => _fakeCommand.Create(A<Guid>.Ignored, A<CreateAdministeredNutrientModel>.Ignored))
                .Throws(new PlantDoesNotExistException(Guid.NewGuid()));

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Create(A<Guid>.Ignored, A<CreateAdministeredNutrientModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST administered plant nutrient returns \"Conflict\" and correct content type if nutrient does not exist")]
        public async Task PostNewAdministeredPlantNutrientReturnsConflictIfNutrientDoesNotExist()
        {
            // Given
            A.CallTo(() => _fakeCommand.Create(A<Guid>.Ignored, A<CreateAdministeredNutrientModel>.Ignored))
                .Throws(new NutrientDoesNotExistException(Guid.NewGuid()));

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Create(A<Guid>.Ignored, A<CreateAdministeredNutrientModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST administered plant nutrient returns \"Conflict\" and correct content type if administration date is earlier than planting date")]
        public async Task PostNewAdministeredPlantNutrientReturnsConflictIfAdministrationDateIsEarlierThanPlantingDate()
        {
            // Given
            A.CallTo(() => _fakeCommand.Create(A<Guid>.Ignored, A<CreateAdministeredNutrientModel>.Ignored))
                .Throws(new NutrientAdministrationDateBeforePlantDateException());

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Create(A<Guid>.Ignored, A<CreateAdministeredNutrientModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST administered plant nutrient returns \"Created\" and sets Location header")]
        public async Task PostNewAdministeredPlantNutrientReturnsCreatedAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Create(A<Guid>.Ignored, A<CreateAdministeredNutrientModel>.Ignored)).Returns(Guid.NewGuid());

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            A.CallTo(() => _fakeCommand.Create(A<Guid>.Ignored, A<CreateAdministeredNutrientModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST administered plant nutrient returns \"Bad Request\" and correct content type for incorrect data")]
        public async Task PostNewAdministeredPlantNutrientReturnsBadRequestAndCorrectContentType()
        {
            // Given
            CreateAdministeredNutrientViewModel model = ViewModelFactory.CreateInvalidCreationModel();

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), model);

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

        [TestCase(TestName = "PUT administered plant nutrient returns \"Conflict\" and correct content type if plant does not exist")]
        public async Task PutAdministeredPlantNutrientReturnsConflictIfPlantDoesNotExistById()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdateAdministeredNutrientModel>.Ignored))
                .Throws(new PlantDoesNotExistException(Guid.NewGuid()));

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdateAdministeredNutrientModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT administered plant nutrient returns \"Conflict\" and correct content type if nutrient does not exist")]
        public async Task PutAdministeredPlantNutrientReturnsConflictIfNutrientDoesNotExistById()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdateAdministeredNutrientModel>.Ignored))
                .Throws(new NutrientDoesNotExistException(Guid.NewGuid()));

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdateAdministeredNutrientModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT administered plant nutrient returns \"Conflict\" and correct content type if administration date is earlier than planting date")]
        public async Task PutAdministeredPlantNutrientReturnsConflictIfAdministrationDateIsEarlierThanPlantingDate()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdateAdministeredNutrientModel>.Ignored))
                .Throws(new NutrientAdministrationDateBeforePlantDateException());

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdateAdministeredNutrientModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT administered plant nutrient returns \"Not Found\" and correct content type if administered plant nutrient does not exist")]
        public async Task PutAdministeredPlantNutrientReturnsNotFoundAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdateAdministeredNutrientModel>.Ignored)).Returns(Task.FromResult<AdministeredNutrient>(null));

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdateAdministeredNutrientModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT administered plant nutrient returns \"OK\" and correct content type")]
        public async Task PutAdministeredPlantNutrientReturnsAdministeredPlantNutrientAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdateAdministeredNutrientModel>.Ignored)).Returns(A.Fake<AdministeredNutrient>());

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommand.Update(A<Guid>.Ignored, A<UpdateAdministeredNutrientModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT administered plant nutrient returns \"BadRequest\" and correct content type")]
        public async Task PutAdministeredPlantNutrientReturnsBadRequestAndCorrectContentType()
        {
            // Given
            UpdateAdministeredNutrientViewModel model = ViewModelFactory.CreateInvalidUpdateModel();

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), model);

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

        [TestCase(TestName = "DELETE administered plant nutrient returns \"No Content\"")]
        public async Task DeleteAdministeredPlantNutrientReturnsNoContentAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommand.Delete(A<Guid>.Ignored, A<Guid>.Ignored)).Returns(Task.CompletedTask);

            // When
            var response = await Client.DeleteAsync(EndPointFactory.DeleteEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            A.CallTo(() => _fakeCommand.Delete(A<Guid>.Ignored, A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET sum of administered plant nutrients returns \"Not Found\" and correct content type if plant does not exist")]
        public async Task GetSumOfAdministeredPlantNutrientsReturnsNotFoundAndCorrectContentTypeIfPlantDoesNotExist()
        {
            // Given
            A.CallTo(() => _fakeQuery.Sum(A<Guid>.Ignored)).Throws(new PlantDoesNotExistException(Guid.NewGuid()));

            // When
            var response = await Client.GetAsync(EndPointFactory.SumTotalNutrientsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Sum(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET sum of administered plant nutrients returns \"OK\" and correct content type")]
        public async Task GetSumOfAdministeredPlantNutrientsReturnsOKAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.Sum(A<Guid>.Ignored)).Returns(A.Fake<IEnumerable<AdministeredPlantNutrientsSummaryModel>>());

            // When
            var response = await Client.GetAsync(EndPointFactory.SumTotalNutrientsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Sum(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}