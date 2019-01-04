using Spice.ViewModels.Plants.Nutrients;
using System;

namespace Spice.WebAPI.Tests.Plants.Nutrients.Factories
{
    internal static class ViewModelFactory
    {
        public static CreateAdministeredNutrientViewModel CreateValidCreationModel()
        {
            return new CreateAdministeredNutrientViewModel
            {
                Date = DateTime.Now.AddHours(-1),
                NutrientId = Guid.NewGuid(),
                Amount = 0.75
            };
        }

        public static CreateAdministeredNutrientViewModel CreateInvalidCreationModel()
        {
            return new CreateAdministeredNutrientViewModel
            {
                Date = DateTime.Now.AddHours(-1),
                NutrientId = Guid.Empty,
                Amount = -0.01
            };
        }

        public static UpdateAdministeredNutrientViewModel CreateValidUpdateModel()
        {
            return new UpdateAdministeredNutrientViewModel
            {
                Date = DateTime.Now.AddHours(1),
                NutrientId = Guid.NewGuid(),
                Amount = 0.75
            };
        }

        public static UpdateAdministeredNutrientViewModel CreateInvalidUpdateModel()
        {
            return new UpdateAdministeredNutrientViewModel
            {
                Date = DateTime.Now.AddHours(1),
                NutrientId = Guid.NewGuid(),
                Amount = -0.01
            };
        }
    }
}