using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Spice.Application.Common.Exceptions;
using Spice.Application.Nutrients.Interfaces;
using Spice.Application.Nutrients.Models;
using Spice.Domain;
using Spice.ViewModels.Nutrients;
using Spice.WebAPI.Tests.Common;
using Spice.WebAPI.Tests.Nutrients.Factories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spice.WebAPI.Tests.Nutrients
{
    [TestFixture]
    internal sealed class IntegrationTests : AbstractIntegrationTestsBaseFixture
    {
        private IQueryNutrients _fakeQuery;
        private ICommandNutrients _fakeCommands;

        protected override void ServicesConfiguration(IServiceCollection services)
        {
            _fakeQuery = A.Fake<IQueryNutrients>();
            services.AddSingleton<IQueryNutrients>(_fakeQuery);

            _fakeCommands = A.Fake<ICommandNutrients>();
            services.AddSingleton<ICommandNutrients>(_fakeCommands);
        }

        [SetUp]
        public void SetUp()
        {
            Fake.ClearRecordedCalls(_fakeQuery);
            Fake.ClearRecordedCalls(_fakeCommands);
        }

        [TestCase(TestName = "GET list of nutrients returns \"OK\" and correct content type")]
        public async Task GetListReturnsNutrientsAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.GetAll()).Returns(A.Fake<IEnumerable<Nutrient>>());

            // When
            var response = await Client.GetAsync(EndPointFactory.ListEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.GetAll()).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET single nutrient returns \"Not Found\" and correct content type")]
        public async Task GetNutrientReturnsNotFoundAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).Returns(Task.FromResult<NutrientDetailsModel>(null));

            // When
            var response = await Client.GetAsync(EndPointFactory.DetailsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "GET single nutrient returns \"OK\" and correct content type")]
        public async Task GetNutrientReturnsNutrientAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).Returns(A.Fake<NutrientDetailsModel>());

            // When
            var response = await Client.GetAsync(EndPointFactory.DetailsEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeQuery.Get(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST Nutrient returns \"Conflict\" and correct content type if resource state exception occured")]
        public async Task PostNewNutrientReturnsConflictIfResourceStateExceptionOccured()
        {
            // Given
            A.CallTo(() => _fakeCommands.Create(A<CreateNutrientModel>.Ignored))
                .Throws(A.Fake<ResourceStateException>());

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(),
                ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommands.Create(A<CreateNutrientModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST Nutrient returns \"Created\" and sets Location header")]
        public async Task PostNewNutrientReturnsCreatedAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommands.Create(A<CreateNutrientModel>.Ignored)).Returns(Guid.NewGuid());

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), ViewModelFactory.CreateValidCreationModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            A.CallTo(() => _fakeCommands.Create(A<CreateNutrientModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "POST Nutrient returns \"Bad Request\" and correct content type for incorrect data")]
        public async Task PostNewNutrientReturnsBadRequestAndCorrectContentType()
        {
            // Given
            CreateNutrientViewModel model = ViewModelFactory.CreateInvalidCreationModel();

            // When
            var response = await Client.PostAsJsonAsync(EndPointFactory.CreateEndpoint(), model);

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

        [TestCase(TestName = "PUT Nutrient returns \"Conflict\" and correct content type if resource state exception occured")]
        public async Task PutNutrientReturnsConflictIfResourceStateExceptionOccured()
        {
            // Given
            A.CallTo(() => _fakeCommands.Update(A<UpdateNutrientModel>.Ignored))
                .Throws(A.Fake<ResourceStateException>());

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(),
                ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommands.Update(A<UpdateNutrientModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT Nutrient returns \"Not Found\" and correct content type if nutrient does not exist")]
        public async Task PutNutrientReturnsNotFoundAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommands.Update(A<UpdateNutrientModel>.Ignored)).Returns(Task.FromResult<Nutrient>(null));

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
            A.CallTo(() => _fakeCommands.Update(A<UpdateNutrientModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT Nutrient returns \"OK\" and correct content type")]
        public async Task PutNutrientReturnsNutrientAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommands.Update(A<UpdateNutrientModel>.Ignored)).Returns(A.Fake<Nutrient>());

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), ViewModelFactory.CreateValidUpdateModel());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            A.CallTo(() => _fakeCommands.Update(A<UpdateNutrientModel>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase(TestName = "PUT Nutrient returns \"Bad Request\" and correct content type")]
        public async Task PutNutrientReturnsBadRequestAndCorrectContentType()
        {
            // Given
            UpdateNutrientViewModel model = ViewModelFactory.CreateInvalidUpdateModel();

            // When
            var response = await Client.PutAsJsonAsync(EndPointFactory.UpdateEndpoint(), model);

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/problem+json; charset=utf-8");
        }

        [TestCase(TestName = "DELETE Nutrient returns \"No Content\"")]
        public async Task DeleteNutrientReturnsNoContentAndCorrectContentType()
        {
            // Given
            A.CallTo(() => _fakeCommands.Delete(A<Guid>.Ignored)).Returns(Task.CompletedTask);

            // When
            var response = await Client.DeleteAsync(EndPointFactory.DeleteEndpoint());

            // Then
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            A.CallTo(() => _fakeCommands.Delete(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}