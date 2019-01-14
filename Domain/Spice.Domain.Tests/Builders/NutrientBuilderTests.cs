using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Builders;

namespace Spice.Domain.Tests.Builders
{
    [TestFixture]
    internal sealed class NutrientBuilderTests : AbstractBaseDomainTestFixture<NutrientBuilder>
    {
        protected override NutrientBuilder CreateDomainObject() => New.Nutrient;

        [TestCase(TestName = "Implicit operator returns nutrient")]
        public void ImplicitOperatorReturnsNutrient()
        {
            // Given
            const string name = "Test nutrient";
            const string description = "Test description of a nutrient";
            const string dosageUnits = "g";
            NutrientBuilder builder = DomainObject;

            // When
            Nutrient nutrient = builder.WithName(name)
                                       .WithDescription(description)
                                       .WithDosageUnits(dosageUnits);

            // Then
            nutrient.Should().NotBeNull();
            nutrient.Name.Should().Be(name);
            nutrient.Description.Should().Be(description);
            nutrient.DosageUnits.Should().Be(dosageUnits);
            nutrient.AdministeredToPlants.Should().NotBeNull();
        }

        [TestCase(TestName = "WithName sets nutrient name")]
        public void WithNameSetsNutrientName()
        {
            // Given
            const string nutrientName = "Test nutrient 123";
            NutrientBuilder builder = DomainObject.WithName(nutrientName);

            // When
            Nutrient newNutrient = builder;

            // Then
            newNutrient.Name.Should().Be(nutrientName);
        }

        [TestCase(TestName = "WithName sets nutrient description")]
        public void WithDescriptionSetsNutrientDescription()
        {
            // Given
            const string nutrientDescription = "Test nutrient 123";
            NutrientBuilder builder = DomainObject.WithDescription(nutrientDescription);

            // When
            Nutrient newNutrient = builder;

            // Then
            newNutrient.Description.Should().Be(nutrientDescription);
        }

        [TestCase(TestName = "WithName sets nutrient dosage unit")]
        public void WithDosageUnitSetsNutrientDosageUnits()
        {
            // Given
            const string nutrientName = "g";
            NutrientBuilder builder = DomainObject.WithDosageUnits(nutrientName);

            // When
            Nutrient newNutrient = builder;

            // Then
            newNutrient.DosageUnits.Should().Be(nutrientName);
        }
    }
}