using System;

namespace Spice.ViewModels.Nutrients
{
    public class NutrientDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DosageUnits { get; set; }
    }
}