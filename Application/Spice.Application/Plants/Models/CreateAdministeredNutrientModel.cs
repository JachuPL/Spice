using System;

namespace Spice.Application.Plants.Models
{
    public class CreateAdministeredNutrientModel
    {
        public Guid NutrientId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}