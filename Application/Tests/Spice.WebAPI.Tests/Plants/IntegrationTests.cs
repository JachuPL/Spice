using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Spice.Application.Plants;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Models;
using Spice.Domain;
using Spice.WebAPI.Plants;
using Spice.WebAPI.Plants.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spice.WebAPI.Tests.Plants
{
    [TestFixture]
    public class IntegrationTests
    {
        private WebApplicationFactory<Startup> _factory;
        private HttpClient _client;
        private IQueryPlants _fakeQueryPlants;
        private ICommandPlants _fakeCommandPlants;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(x =>
                {
                    x.ConfigureTestServices(ServicesConfiguration);
                });

            _client = _factory.CreateClient();
        }

        [SetUp]
        public void SetUp()
        {
            Fake.ClearRecordedCalls(_fakeQueryPlants);
            Fake.ClearRecordedCalls(_fakeCommandPlants);
        }

        private void ServicesConfiguration(IServiceCollection services)
        {
            _fakeQueryPlants = A.Fake<IQueryPlants>();
            services.AddSingleton<IQueryPlants>(_fakeQueryPlants);

            _fakeCommandPlants = A.Fake<ICommandPlants>();
            services.AddSingleton<ICommandPlants>(_fakeCommandPlants);
        }

        [TestCase(TestName = "GET list of plants returns \"OK\" and correct content type")]
        public async Task GetListReturnsPlantsAndCorrectContentType()
        {
            string remoteEndPoint = "/api/plants";
            A.CallTo(() => _fakeQueryPlants.GetAll()).Returns(A.Fake<IEnumerable<Plant>>());

            // Act
            var response = await _client.GetAsync(remoteEndPoint);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQueryPlants.GetAll()).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET single plant returns \"Not Found\" and correct content type")]
        public async Task GetPlantReturnsNotFoundAndCorrectContentType()
        {
            // Arrange
            A.CallTo(() => _fakeQueryPlants.Get(A<Guid>.Ignored)).Returns(Task.FromResult<Plant>(null));
            string remoteEndPoint = "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";

            // Act
            var response = await _client.GetAsync(remoteEndPoint);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeQueryPlants.Get(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET single plant returns \"OK\" and correct content type")]
        public async Task GetPlantReturnsPlantAndCorrectContentType()
        {
            // Arrange
            A.CallTo(() => _fakeQueryPlants.Get(A<Guid>.Ignored)).Returns(A.Fake<Plant>());
            string remoteEndPoint = "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";

            // Act
            var response = await _client.GetAsync(remoteEndPoint);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQueryPlants.Get(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST plant returns \"Conflict\" and correct content type if plant exists at specific coordinates")]
        public async Task PostNewPlantReturnsConflictIfPlantExistsAtSpecificCoordinates()
        {
            // Arrange
            A.CallTo(() => _fakeCommandPlants.Create(A<CreatePlantModel>.Ignored))
                .Throws(new PlantExistsAtCoordinatesException(0, 0));
            string remoteEndPoint = "/api/plants";
            CreatePlantViewModel model = new CreatePlantViewModel()
            {
                Name = "Pepper",
                Specimen = "Capsicum annuum",
                FieldName = "Field A",
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = PlantStateViewModelEnum.Healthy
            };

            // Act
            var response = await _client.PostAsJsonAsync(remoteEndPoint, model);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommandPlants.Create(A<CreatePlantModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST plant returns \"Created\", correct content type and sets Location header")]
        public async Task PostNewPlantReturnsCreatedAndCorrectContentType()
        {
            // Arrange
            A.CallTo(() => _fakeCommandPlants.Create(A<CreatePlantModel>.Ignored)).Returns(Guid.NewGuid());
            string remoteEndPoint = "/api/plants";
            CreatePlantViewModel model = new CreatePlantViewModel()
            {
                Name = "Pepper",
                Specimen = "Capsicum annuum",
                FieldName = "Field A",
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = PlantStateViewModelEnum.Healthy
            };

            // Act
            var response = await _client.PostAsJsonAsync(remoteEndPoint, model);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommandPlants.Create(A<CreatePlantModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST plant returns \"Bad Request\" and correct content type for incorrect data")]
        public async Task PostNewPlantReturnsBadRequestAndCorrectContentType()
        {
            // Arrange
            string remoteEndPoint = "/api/plants";
            CreatePlantViewModel model = new CreatePlantViewModel()
            {
                Name = string.Empty,
                Specimen = string.Empty,
                FieldName = string.Empty,
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = (PlantStateViewModelEnum)999
            };

            // Act
            var response = await _client.PostAsJsonAsync(remoteEndPoint, model);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

        [TestCase(TestName = "PUT plant returns \"Conflict\" and correct content type if other plant at specified coordinates already exists")]
        public async Task PutPlantReturnsConflictIfNewCoordinatesConflictWithExistingPlant()
        {
            // Arrange
            A.CallTo(() => _fakeCommandPlants.Update(A<UpdatePlantModel>.Ignored))
                .Throws(new PlantExistsAtCoordinatesException(0, 0));
            string remoteEndPoint = "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";
            UpdatePlantViewModel model = new UpdatePlantViewModel()
            {
                Name = "Pepper",
                Specimen = "Capsicum annuum",
                FieldName = "Field A",
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = PlantStateViewModelEnum.Healthy
            };

            // Act
            var response = await _client.PutAsJsonAsync(remoteEndPoint, model);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommandPlants.Update(A<UpdatePlantModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT plant returns \"Not Found\" and correct content type if plant was not found")]
        public async Task PutPlantReturnsNotFoundAndCorrectContentType()
        {
            // Arrange
            A.CallTo(() => _fakeCommandPlants.Update(A<UpdatePlantModel>.Ignored)).Returns(Task.FromResult<Plant>(null));
            string remoteEndPoint = "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";
            UpdatePlantViewModel model = new UpdatePlantViewModel()
            {
                Name = "Pepper",
                Specimen = "Capsicum annuum",
                FieldName = "Field A",
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = PlantStateViewModelEnum.Healthy
            };

            // Act
            var response = await _client.PutAsJsonAsync(remoteEndPoint, model);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeCommandPlants.Update(A<UpdatePlantModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT plant returns \"OK\" and correct content type")]
        public async Task PutPlantReturnsPlantAndCorrectContentType()
        {
            // Arrange
            A.CallTo(() => _fakeCommandPlants.Update(A<UpdatePlantModel>.Ignored)).Returns(A.Fake<Plant>());
            string remoteEndPoint = "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";
            UpdatePlantViewModel model = new UpdatePlantViewModel()
            {
                Name = "Pepper",
                Specimen = "Capsicum annuum",
                FieldName = "Field A",
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = PlantStateViewModelEnum.Healthy
            };

            // Act
            var response = await _client.PutAsJsonAsync(remoteEndPoint, model);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommandPlants.Update(A<UpdatePlantModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT plant returns \"BadRequest\" and correct content type")]
        public async Task PutPlantReturnsBadRequestAndCorrectContentType()
        {
            // Arrange
            string remoteEndPoint = "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";
            UpdatePlantViewModel model = new UpdatePlantViewModel()
            {
                Name = string.Empty,
                Specimen = string.Empty,
                FieldName = string.Empty,
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = (PlantStateViewModelEnum)999
            };

            // Act
            var response = await _client.PutAsJsonAsync(remoteEndPoint, model);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

        [TestCase(TestName = "DELETE plant returns \"No Content\"")]
        public async Task DeletePlantReturnsNoContentAndCorrectContentType()
        {
            // Arrange
            A.CallTo(() => _fakeCommandPlants.Delete(A<Guid>.Ignored)).Returns(Task.CompletedTask);
            string remoteEndPoint = "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";

            // Act
            var response = await _client.DeleteAsync(remoteEndPoint);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            A.CallTo(() => _fakeCommandPlants.Delete(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}