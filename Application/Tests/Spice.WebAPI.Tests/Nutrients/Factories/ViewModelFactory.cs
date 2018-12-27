using Spice.ViewModels.Nutrients;

namespace Spice.WebAPI.Tests.Nutrients.Factories
{
    internal static class ViewModelFactory
    {
        public static CreateNutrientViewModel CreateValidCreationModel()
        {
            return new CreateNutrientViewModel()
            {
                Name = "Mineral water",
                Description = "Either tap or bottled.",
                DosageUnits = "ml"
            };
        }

        public static CreateNutrientViewModel CreateInvalidCreationModel()
        {
            return new CreateNutrientViewModel()
            {
                Name = "A",
                Description = "B",
                DosageUnits = "C"
            };
        }

        public static UpdateNutrientViewModel CreateValidUpdateModel()
        {
            return new UpdateNutrientViewModel()
            {
                Name = "Mineral water",
                Description = "Either tap or bottled.",
                DosageUnits = "ml"
            };
        }

        public static UpdateNutrientViewModel CreateInvalidUpdateModel()
        {
            return new UpdateNutrientViewModel()
            {
                Name = "A",
                Description = "B",
                DosageUnits = "C"
            };
        }
    }
}