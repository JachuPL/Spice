using Spice.Application.Nutrients.Models;

namespace Spice.Application.Plants.Nutrients.Models
{
    public class AdministeredPlantNutrientsSummaryModel
    {
        public NutrientDetailsModel Nutrient { get; set; }
        public double TotalAmount { get; set; }
    }
}