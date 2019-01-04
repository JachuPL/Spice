using Spice.ViewModels.Plants;
using System;

namespace Spice.WebAPI.Tests.Plants.Factories
{
    internal static class ViewModelFactory
    {
        public static CreatePlantViewModel CreateValidCreationModel()
        {
            return new CreatePlantViewModel
            {
                Name = "Pepper",
                SpeciesId = Guid.NewGuid(),
                FieldId = Guid.NewGuid(),
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = PlantStateViewModel.Healthy
            };
        }

        public static CreatePlantViewModel CreateInvalidCreationModel()
        {
            return new CreatePlantViewModel
            {
                Name = string.Empty,
                SpeciesId = Guid.Empty,
                FieldId = Guid.Empty,
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = (PlantStateViewModel)999
            };
        }

        public static UpdatePlantViewModel CreateValidUpdateModel()
        {
            return new UpdatePlantViewModel
            {
                Name = "Pepper",
                SpeciesId = Guid.NewGuid(),
                FieldId = Guid.NewGuid(),
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = PlantStateViewModel.Healthy
            };
        }

        public static UpdatePlantViewModel CreateInvalidUpdateModel()
        {
            return new UpdatePlantViewModel
            {
                Name = string.Empty,
                SpeciesId = Guid.Empty,
                FieldId = Guid.Empty,
                Row = 0,
                Column = 0,
                Planted = DateTime.Now,
                State = (PlantStateViewModel)999
            };
        }
    }
}