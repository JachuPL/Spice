using Spice.Application.Common.Exceptions;

namespace Spice.Application.Nutrients.Exceptions
{
    public class NutrientWithNameAlreadyExistsException : ResourceStateException
    {
        public NutrientWithNameAlreadyExistsException(string name) : base($"Nutrient with name {name} already exists.")
        {
        }
    }
}