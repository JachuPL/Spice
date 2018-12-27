using Spice.ViewModels.Nutrients;

namespace Spice.WebAPI.Tests.Nutrients.Factories
{
    internal static class ViewModelFactory
    {
        public static CreateNutrientViewModel CreateValidCreationModel()
        {
            return new CreateNutrientViewModel()
            {
            };
        }

        public static CreateNutrientViewModel CreateInvalidCreationModel()
        {
            return new CreateNutrientViewModel()
            {
            };
        }

        public static UpdateNutrientViewModel CreateValidUpdateModel()
        {
            return new UpdateNutrientViewModel()
            {
            };
        }

        public static UpdateNutrientViewModel CreateInvalidUpdateModel()
        {
            return new UpdateNutrientViewModel()
            {
            };
        }
    }
}