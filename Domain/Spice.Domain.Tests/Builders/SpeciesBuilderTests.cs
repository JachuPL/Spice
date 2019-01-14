using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Builders;

namespace Spice.Domain.Tests.Builders
{
    internal sealed class SpeciesBuilderTests : AbstractBaseDomainTestFixture<SpeciesBuilder>
    {
        protected override SpeciesBuilder CreateDomainObject() => New.Species;

        [TestCase(TestName = "Implicit operator returns species")]
        public void ImplicitOperatorReturnsSpecies()
        {
            // Given
            const string name = "Test species";
            const string latinName = "Latin name of a species";
            const string description = "Test description of a species";
            SpeciesBuilder builder = DomainObject;

            // When
            Species species = builder.WithName(name)
                                     .WithLatinName(latinName)
                                       .WithDescription(description);

            // Then
            species.Should().NotBeNull();
            species.Name.Should().Be(name);
            species.LatinName.Should().Be(latinName);
            species.Description.Should().Be(description);
            species.Plants.Should().NotBeNull();
        }

        [TestCase(TestName = "WithName sets species name")]
        public void WithNameSetsSpeciesName()
        {
            // Given
            const string speciesName = "Test species 123";
            SpeciesBuilder builder = DomainObject.WithName(speciesName);

            // When
            Species newSpecies = builder;

            // Then
            newSpecies.Name.Should().Be(speciesName);
        }

        [TestCase(TestName = "WithName sets species latin name")]
        public void WithDescriptionSetsSpeciesLatinName()
        {
            // Given
            const string speciesLatinName = "Test species 123";
            SpeciesBuilder builder = DomainObject.WithLatinName(speciesLatinName);

            // When
            Species newSpecies = builder;

            // Then
            newSpecies.LatinName.Should().Be(speciesLatinName);
        }

        [TestCase(TestName = "WithName sets species description")]
        public void WithDosageUnitSetsSpeciesDescription()
        {
            // Given
            const string speciesDescription = "Test species 123";
            SpeciesBuilder builder = DomainObject.WithDescription(speciesDescription);

            // When
            Species newSpecies = builder;

            // Then
            newSpecies.Description.Should().Be(speciesDescription);
        }
    }
}