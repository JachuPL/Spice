using System;

namespace Spice.Application.Nutrients.Models
{
    public class UpdateNutrientModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DosageUnits { get; set; }
    }
}