using Spice.Application.Nutrients.Models;
using System;

namespace Spice.Application.Species.Models
{
    public class SpeciesNutritionSummaryModel
    {
        public NutrientDetailsModel Nutrient { get; set; }
        public double TotalAmount { get; set; }
        public DateTime FirstAdministration { get; set; }
        public DateTime LastAdministration { get; set; }
    }
}