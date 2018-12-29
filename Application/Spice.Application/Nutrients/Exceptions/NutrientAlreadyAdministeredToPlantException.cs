using System;

namespace Spice.Application.Nutrients.Exceptions
{
    public class NutrientAlreadyAdministeredToPlantException : InvalidOperationException
    {
        public NutrientAlreadyAdministeredToPlantException() : base("Nutrient cannot be edited since it was administered to a plant.")
        {
            
        }
    }
}