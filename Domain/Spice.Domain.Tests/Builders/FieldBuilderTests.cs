using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Builders;

namespace Spice.Domain.Tests.Builders
{
    [TestFixture]
    internal sealed class FieldBuilderTests : AbstractBaseDomainTestFixture<FieldBuilder>
    {
        protected override FieldBuilder CreateDomainObject() => New.Field;

        [TestCase(TestName = "Implicit operator returns field")]
        public void ImplicitOperatorReturnsField()
        {
            // Given
            const string name = "Test field";
            const string description = "Test description of a field";
            const double latitude = 52;
            const double longtitude = 21;
            FieldBuilder builder = DomainObject;

            // When
            Field field = builder.WithName(name)
                                 .WithDescription(description)
                                 .OnLatitude(latitude).OnLongtitude(longtitude);

            // Then
            field.Should().NotBeNull();
            field.Name.Should().Be(name);
            field.Description.Should().Be(description);
            field.Latitude.Should().Be(latitude);
            field.Longtitude.Should().Be(longtitude);
            field.Plants.Should().NotBeNull();
        }

        [TestCase(TestName = "WithName sets field name")]
        public void WithNameSetsFieldName()
        {
            // Given
            const string fieldName = "Test field 123";
            FieldBuilder builder = DomainObject.WithName(fieldName);

            // When
            Field newField = builder;

            // Then
            newField.Name.Should().Be(fieldName);
        }

        [TestCase(TestName = "WithDescription sets field description")]
        public void WithDescriptionSetsFieldDescription()
        {
            // Given
            const string fieldDesciption = "Test field description 123";
            FieldBuilder builder = DomainObject.WithDescription(fieldDesciption);

            // When
            Field newField = builder;

            // Then
            newField.Description.Should().Be(fieldDesciption);
        }

        [TestCase(TestName = "OnLatitude sets field latitude")]
        public void OnLatitudeSetsFieldLatitude()
        {
            // Given
            const double latitude = 44;
            FieldBuilder builder = DomainObject.OnLatitude(latitude);

            // When
            Field newField = builder;

            // Then
            newField.Latitude.Should().Be(latitude);
        }

        [TestCase(TestName = "OnLongtitude sets field longtitude")]
        public void OnLongtitudeSetsFieldLongtitude()
        {
            // Given
            const double longtitude = 44;
            FieldBuilder builder = DomainObject.OnLongtitude(longtitude);

            // When
            Field newField = builder;

            // Then
            newField.Longtitude.Should().Be(longtitude);
        }
    }
}