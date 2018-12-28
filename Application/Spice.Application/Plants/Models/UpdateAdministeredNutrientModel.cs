using System;

namespace Spice.Application.Plants.Models
{
    public class UpdateAdministeredNutrientModel
    {
        public Guid Id { get; set; }
        public Guid NutrientId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}