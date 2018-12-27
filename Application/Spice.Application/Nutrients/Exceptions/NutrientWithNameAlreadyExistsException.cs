using System;

namespace Spice.Application.Nutrients.Exceptions
{
    public class NutrientWithNameAlreadyExistsException : ArgumentException
    {
        public NutrientWithNameAlreadyExistsException(string name) : base($"Nutrient with name {name} already exists.")
        {
        }
    }
}