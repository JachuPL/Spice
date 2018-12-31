using Spice.Application.Common.Exceptions;
using Spice.Domain.Plants;
using System;

namespace Spice.Application.Plants.Exceptions
{
    public class PlantNotFoundException : ResourceNotFoundException
    {
        public PlantNotFoundException(Guid id) : base(typeof(Plant), id)
        {
        }
    }
}