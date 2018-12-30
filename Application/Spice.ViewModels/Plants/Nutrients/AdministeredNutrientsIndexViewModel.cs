using System;

namespace Spice.ViewModels.Plants.Nutrients
{
    public class AdministeredNutrientsIndexViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public DateTime Date { get; set; }
    }
}