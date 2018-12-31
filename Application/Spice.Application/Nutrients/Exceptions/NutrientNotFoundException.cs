using Spice.Application.Common.Exceptions;
using Spice.Domain;
using System;

namespace Spice.Application.Nutrients.Exceptions
{
    public class NutrientNotFoundException : ResourceNotFoundException
    {
        public NutrientNotFoundException(Guid id) : base(typeof(Nutrient), id)
        {
        }
    }
}