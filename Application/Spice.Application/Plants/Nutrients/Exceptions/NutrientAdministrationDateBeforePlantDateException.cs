using Spice.Application.Common.Exceptions;

namespace Spice.Application.Plants.Nutrients.Exceptions
{
    public class NutrientAdministrationDateBeforePlantDateException : ResourceStateException
    {
        public NutrientAdministrationDateBeforePlantDateException() : base("Nutrient application date cannot be before plant date.")
        {
        }
    }
}