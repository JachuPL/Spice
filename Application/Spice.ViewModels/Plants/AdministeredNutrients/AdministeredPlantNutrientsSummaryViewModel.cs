﻿using Spice.ViewModels.Nutrients;

namespace Spice.ViewModels.Plants.AdministeredNutrients
{
    public class AdministeredPlantNutrientsSummaryViewModel
    {
        public NutrientDetailsViewModel Nutrient { get; set; }
        public double TotalAmount { get; set; }
    }
}