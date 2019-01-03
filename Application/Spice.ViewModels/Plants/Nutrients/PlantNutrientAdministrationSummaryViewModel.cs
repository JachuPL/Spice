using Spice.ViewModels.Nutrients;
using System;

namespace Spice.ViewModels.Plants.Nutrients
{
    public class PlantNutrientAdministrationSummaryViewModel
    {
        public NutrientDetailsViewModel Nutrient { get; set; }
        public double TotalAmount { get; set; }
        public DateTime FirstAdministration { get; set; }
        public DateTime LastAdministration { get; set; }
    }
}