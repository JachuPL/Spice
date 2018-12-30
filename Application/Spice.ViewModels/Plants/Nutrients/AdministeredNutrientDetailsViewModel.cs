using Spice.ViewModels.Nutrients;
using System;

namespace Spice.ViewModels.Plants.Nutrients
{
    public class AdministeredNutrientDetailsViewModel
    {
        public Guid Id { get; set; }
        public NutrientDetailsViewModel Nutrient { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}