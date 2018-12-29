using System;

namespace Spice.ViewModels.Plants.AdministeredNutrients
{
    public class AdministeredNutrientsIndexViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public DateTime Date { get; set; }
    }
}