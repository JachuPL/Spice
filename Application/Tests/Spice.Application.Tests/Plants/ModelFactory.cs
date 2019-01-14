using Spice.Application.Plants.Models;
using Spice.Domain;
using Spice.Domain.Builders;
using Spice.Domain.Plants;
using System;

namespace Spice.Application.Tests.Plants
{
    internal static class ModelFactory
    {
        public static CreatePlantModel CreationModel(Guid? fieldId = null, Guid? speciesId = null)
        {
            return new CreatePlantModel
            {
                Name = "Rocoto Giant Red",
                SpeciesId = speciesId ?? Guid.NewGuid(),
                FieldId = fieldId ?? Guid.NewGuid(),
                Column = 0,
                Row = 0,
                Planted = DateTime.Now,
                State = PlantState.Healthy
            };
        }

        public static UpdatePlantModel UpdateModel(Guid? id = null, Guid? fieldId = null, Guid? speciesId = null)
        {
            return new UpdatePlantModel
            {
                Id = id ?? Guid.NewGuid(),
                Name = "Rocoto Giant Red",
                SpeciesId = speciesId ?? Guid.NewGuid(),
                FieldId = fieldId ?? Guid.NewGuid(),
                Column = 0,
                Row = 0,
                Planted = DateTime.Now,
                State = PlantState.Healthy
            };
        }

        public static Plant DomainModel(Field field = null, Domain.Species species = null, string name = "Rocoto Giant Red", int row = 0, int col = 0, PlantState state = PlantState.Healthy)
        {
            return New.Plant.WithName(name)
                             .WithSpecies(species ?? New.Species.WithName("Test species"))
                             .WithField(field ?? New.Field.WithName("Test field"))
                             .InRow(row)
                             .InColumn(col)
                             .WithState(state);
        }
    }
}