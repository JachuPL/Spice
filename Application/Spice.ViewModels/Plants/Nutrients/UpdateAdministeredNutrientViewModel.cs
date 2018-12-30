using System;

namespace Spice.ViewModels.Plants.Nutrients
{
    public class UpdateAdministeredNutrientViewModel
    {
        public Guid NutrientId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}