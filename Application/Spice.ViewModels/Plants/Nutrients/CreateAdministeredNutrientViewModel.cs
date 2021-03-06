﻿using System;

namespace Spice.ViewModels.Plants.Nutrients
{
    public class CreateAdministeredNutrientViewModel
    {
        public Guid NutrientId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool CreateEvent { get; set; }
    }
}