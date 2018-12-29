using System;

namespace Spice.Application.Plants.Exceptions
{
    public class NutrientApplicationDateBeforePlantDateException : ArgumentException
    {
        public NutrientApplicationDateBeforePlantDateException() : base("Nutrient application date cannot be before plant date.")
        {
        }
    }
}