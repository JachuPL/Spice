using FluentAssertions;
using NUnit.Framework;
using Spice.Domain.Builders;
using Spice.Domain.Plants;
using System;
using System.Data;

namespace Spice.Domain.Tests.Builders
{
    internal sealed class PlantBuilderTests : AbstractBaseDomainTestFixture<PlantBuilder>
    {
        protected override PlantBuilder CreateDomainObject() => New.Plant;

        [TestCase(TestName = "Implicit operator returns plant")]
        public void ImplicitOperatorReturnsSpecies()
        {
            // Given
            const string name = "Test species";
            Species species = New.Species.WithName("Test species");
            Field field = New.Field.WithName("Test field");
            const int row = 0;
            const int column = 0;
            const PlantState state = PlantState.Fruiting;
            PlantBuilder builder = DomainObject;

            // When
            Plant plant = builder.WithName(name)
                                 .WithSpecies(species)
                                 .WithField(field)
                                 .InRow(row).InColumn(column)
                                 .WithState(state);

            // Then
            plant.Should().NotBeNull();
            plant.Name.Should().Be(name);
            plant.Species.Should().Be(species);
            plant.Field.Should().Be(field);
            plant.Row.Should().Be(row);
            plant.Column.Should().Be(column);
            plant.State.Should().Be(state);
            plant.Events.Should().NotBeNullOrEmpty();
            plant.AdministeredNutrients.Should().NotBeNull();
        }

        [TestCase(TestName = "Implicit operator throws exception when field is null")]
        public void ImplicitOperatorThrowsExceptionWhenFieldIsNull()
        {
            // Given
            const string name = "Test species";
            Species species = New.Species.WithName("Test species");
            const int row = 0;
            const int column = 0;
            const PlantState state = PlantState.Fruiting;
            PlantBuilder builder = DomainObject;

            // When
            Action createPlant = () =>
                                 {
                                     Plant plant = builder.WithName(name)
                                                          .WithSpecies(species)
                                                          .InRow(row).InColumn(column)
                                                          .WithState(state);
                                     plant.Should().BeNull();
                                 };

            // Then
            createPlant.Should().Throw<NoNullAllowedException>();
        }

        [TestCase(TestName = "Implicit operator throws exception when species is null")]
        public void ImplicitOperatorThrowsExceptionWhenSpeciesIsNull()
        {
            // Given
            const string name = "Test species";
            Field field = New.Field.WithName("Test field");
            const int row = 0;
            const int column = 0;
            const PlantState state = PlantState.Fruiting;
            PlantBuilder builder = DomainObject;

            // When
            Action createPlant = () =>
                                 {
                                     Plant plant = builder.WithName(name)
                                                          .WithField(field)
                                                          .InRow(row).InColumn(column)
                                                          .WithState(state);
                                     plant.Should().BeNull();
                                 };

            // Then
            createPlant.Should().Throw<NoNullAllowedException>();
        }

        [TestCase(TestName = "WithName sets plant name")]
        public void WithNameSetsPlantName()
        {
            // Given
            const string plantName = "Test Plant 123";
            PlantBuilder builder = DomainObject.WithName(plantName).WithSpecies(New.Species.WithName("Test species"))
                                               .WithField(New.Field.WithName("Test species"));

            // When
            Plant newPlant = builder;

            // Then
            newPlant.Name.Should().Be(plantName);
        }

        [TestCase(TestName = "WithSpecies sets plant species")]
        public void WithSpeciesSetsPlantSpecies()
        {
            // Given
            Species species = New.Species.WithName("Test species");
            PlantBuilder builder = DomainObject.WithSpecies(species).WithField(New.Field.WithName("Test field"));

            // When
            Plant newPlant = builder;

            // Then
            newPlant.Species.Should().Be(species);
        }

        [TestCase(TestName = "WithField sets plant field")]
        public void WithFieldSetsPlantField()
        {
            // Given
            Field field = New.Field.WithName("Test field");
            PlantBuilder builder = DomainObject.WithField(field).WithSpecies(New.Species.WithName("Test species"));

            // When
            Plant newPlant = builder;

            // Then
            newPlant.Field.Should().Be(field);
        }

        [TestCase(TestName = "InRow sets plant row")]
        public void InRowSetsPlantRow()
        {
            // Given
            int row = 0;
            PlantBuilder builder = DomainObject.InRow(row).WithSpecies(New.Species.WithName("Test species"))
                                               .WithField(New.Field.WithName("Test species"));

            // When
            Plant newPlant = builder;

            // Then
            newPlant.Row.Should().Be(row);
        }

        [TestCase(TestName = "InColumn sets plant column")]
        public void InColumnSetsPlantRow()
        {
            // Given
            int column = 0;
            PlantBuilder builder = DomainObject.InColumn(column).WithSpecies(New.Species.WithName("Test species"))
                                               .WithField(New.Field.WithName("Test species"));

            // When
            Plant newPlant = builder;

            // Then
            newPlant.Column.Should().Be(column);
        }

        [TestCase(TestName = "WithState sets plant state")]
        public void WithStateSetsPlantState()
        {
            // Given
            PlantState state = PlantState.Fruiting;
            PlantBuilder builder = DomainObject.WithState(state).WithSpecies(New.Species.WithName("Test species"))
                                               .WithField(New.Field.WithName("Test species"));

            // When
            Plant newPlant = builder;

            // Then
            newPlant.State.Should().Be(state);
        }
    }
}