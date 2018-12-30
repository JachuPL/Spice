using System;

namespace Spice.Application.Plants.Nutrients.Exceptions
{
    public class NutrientAdministrationDateBeforePlantDateException : ArgumentException
    {
        public NutrientAdministrationDateBeforePlantDateException() : base("Nutrient application date cannot be before plant date.")
        {
        }
    }
}