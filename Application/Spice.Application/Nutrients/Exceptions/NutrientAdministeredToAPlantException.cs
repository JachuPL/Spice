using System;

namespace Spice.Application.Nutrients.Exceptions
{
    public class NutrientAdministeredToAPlantException : InvalidOperationException
    {
        public NutrientAdministeredToAPlantException() : base("Nutrient cannot be edited since it was administered to a plant.")
        {
        }
    }
}