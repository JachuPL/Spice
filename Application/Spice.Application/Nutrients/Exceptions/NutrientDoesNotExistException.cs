using System;

namespace Spice.Application.Nutrients.Exceptions
{
    public class NutrientDoesNotExistException : ArgumentException
    {
        public NutrientDoesNotExistException(Guid id) : base($"No nutrient by id {id}")
        {
        }
    }
}