using Spice.ViewModels.Nutrients;

namespace Spice.ViewModels.Plants.Nutrients
{
    public class AdministeredPlantNutrientsSummaryViewModel
    {
        public NutrientDetailsViewModel Nutrient { get; set; }
        public double TotalAmount { get; set; }
    }
}