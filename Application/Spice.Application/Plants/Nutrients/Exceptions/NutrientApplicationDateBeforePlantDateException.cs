using System;

namespace Spice.Application.Plants.Nutrients.Exceptions
{
    public class NutrientApplicationDateBeforePlantDateException : ArgumentException
    {
        public NutrientApplicationDateBeforePlantDateException() : base("Nutrient application date cannot be before plant date.")
        {
        }
    }
}