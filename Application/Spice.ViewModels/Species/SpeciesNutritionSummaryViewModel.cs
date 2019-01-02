using Spice.ViewModels.Nutrients;
using System;

namespace Spice.ViewModels.Species
{
    public class SpeciesNutritionSummaryViewModel
    {
        public NutrientDetailsViewModel Nutrient { get; set; }
        public double TotalAmount { get; set; }
        public DateTime FirstAdministration { get; set; }
        public DateTime LastAdministration { get; set; }
    }
}