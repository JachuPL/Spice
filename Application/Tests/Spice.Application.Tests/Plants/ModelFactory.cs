﻿using Spice.Application.Plants.Models;
using Spice.Domain;
using Spice.Domain.Plants;
using System;

namespace Spice.Application.Tests.Plants
{
    public static class ModelFactory
    {
        public static CreatePlantModel CreationModel(Guid? fieldId = null, Guid? speciesId = null)
        {
            return new CreatePlantModel()
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
            return new UpdatePlantModel()
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

        public static Plant DomainModel(Field field = null, Domain.Plants.Species species = null, int row = 0, int col = 0)
        {
            return new Plant()
            {
                Name = "Rocoto Giant Red",
                Species = species ?? Species.ModelFactory.DomainModel(),
                Field = field ?? Fields.ModelFactory.DomainModel(),
                Column = col,
                Row = row,
                Planted = DateTime.Now,
                State = PlantState.Healthy
            };
        }
    }
}