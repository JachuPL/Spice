﻿using Spice.Application.Common.Exceptions;

namespace Spice.Application.Nutrients.Exceptions
{
    public class NutrientAdministeredToAPlantException : ResourceStateException
    {
        public NutrientAdministeredToAPlantException() : base("Nutrient cannot be edited since it was administered to a plant.")
        {
        }
    }
}