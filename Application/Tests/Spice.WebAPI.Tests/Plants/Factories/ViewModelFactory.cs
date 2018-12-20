using Spice.WebAPI.Plants.Models;
using System;

namespace Spice.WebAPI.Tests.Plants.Factories
{
    internal static class ViewModelFactory
    {
        public static CreatePlantViewModel CreateValidCreationModel()
        {
            return new CreatePlantViewModel()
            {
                Name = "Pepper",
                Specimen = "Capsicum annuum",
                FieldName = "Field A",
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = PlantStateViewModelEnum.Healthy
            };
        }

        public static CreatePlantViewModel CreateInvalidCreationModel()
        {
            return new CreatePlantViewModel()
            {
                Name = string.Empty,
                Specimen = string.Empty,
                FieldName = string.Empty,
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = (PlantStateViewModelEnum)999
            };
        }

        public static UpdatePlantViewModel CreateValidUpdateModel()
        {
            return new UpdatePlantViewModel()
            {
                Name = "Pepper",
                Specimen = "Capsicum annuum",
                FieldName = "Field A",
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = PlantStateViewModelEnum.Healthy
            };
        }

        public static UpdatePlantViewModel CreateInvalidUpdateModel()
        {
            return new UpdatePlantViewModel()
            {
                Name = string.Empty,
                Specimen = string.Empty,
                FieldName = string.Empty,
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = (PlantStateViewModelEnum)999
            };
        }
    }
}