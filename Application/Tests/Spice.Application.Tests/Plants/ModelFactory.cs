using Spice.Application.Plants.Models;
using Spice.Domain;
using System;

namespace Spice.Application.Tests.Plants
{
    public static class ModelFactory
    {
        public static CreatePlantModel CreationModel()
        {
            return new CreatePlantModel()
            {
                Name = "Rocoto Giant Red",
                Specimen = "Capsicum annuum",
                FieldName = "Field A",
                Column = 0,
                Row = 0,
                Planted = DateTime.Now,
                State = PlantState.Healthy
            };
        }

        public static UpdatePlantModel UpdateModel(Guid? id = null)
        {
            return new UpdatePlantModel()
            {
                Id = id ?? Guid.NewGuid(),
                Name = "Rocoto Giant Red",
                Specimen = "Capsicum annuum",
                FieldName = "Field A",
                Column = 0,
                Row = 0,
                Planted = DateTime.Now,
                State = PlantState.Healthy
            };
        }

        public static Plant DomainModel(string fieldName = "Field A", int row = 0, int col = 0)
        {
            return new Plant()
            {
                Name = "Rocoto Giant Red",
                Specimen = "Capsicum annuum",
                FieldName = fieldName,
                Column = col,
                Row = row,
                Planted = DateTime.Now,
                State = PlantState.Healthy
            };
        }
    }
}