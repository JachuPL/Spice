using Spice.Application.Nutrients.Models;
using System;

namespace Spice.Application.Plants.Nutrients.Models
{
    public class PlantNutrientAdministrationSummaryModel
    {
        public NutrientDetailsModel Nutrient { get; set; }
        public double TotalAmount { get; set; }
        public DateTime FirstAdministration { get; set; }
        public DateTime LastAdministration { get; set; }
    }
}