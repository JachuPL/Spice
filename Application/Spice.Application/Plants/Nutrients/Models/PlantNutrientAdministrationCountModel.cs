using Spice.Application.Nutrients.Models;

namespace Spice.Application.Plants.Nutrients.Models
{
    public class PlantNutrientAdministrationCountModel
    {
        public NutrientDetailsModel Nutrient { get; set; }
        public double TotalAmount { get; set; }
    }
}