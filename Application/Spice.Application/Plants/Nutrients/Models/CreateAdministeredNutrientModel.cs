using System;

namespace Spice.Application.Plants.Nutrients.Models
{
    public class CreateAdministeredNutrientModel
    {
        public Guid NutrientId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public bool CreateEvent { get; set; } = false;
    }
}