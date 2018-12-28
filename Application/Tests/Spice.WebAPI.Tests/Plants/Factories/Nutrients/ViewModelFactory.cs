using Spice.ViewModels.Plants.AdministeredNutrients;

namespace Spice.WebAPI.Tests.Plants.Factories.Nutrients
{
    internal static class ViewModelFactory
    {
        public static CreateAdministeredNutrientViewModel CreateValidCreationModel()
        {
            return new CreateAdministeredNutrientViewModel()
            {
            };
        }

        public static CreateAdministeredNutrientViewModel CreateInvalidCreationModel()
        {
            return new CreateAdministeredNutrientViewModel()
            {
            };
        }

        public static UpdateAdministeredNutrientViewModel CreateValidUpdateModel()
        {
            return new UpdateAdministeredNutrientViewModel()
            {
            };
        }

        public static UpdateAdministeredNutrientViewModel CreateInvalidUpdateModel()
        {
            return new UpdateAdministeredNutrientViewModel()
            {
            };
        }
    }
}