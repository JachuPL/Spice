using Spice.Application.Common.Exceptions;
using System;

namespace Spice.Application.Species.Exceptions
{
    public class SpeciesNotFoundException : ResourceNotFoundException
    {
        public SpeciesNotFoundException(Guid id) : base(typeof(Domain.Plants.Species), id)
        {
        }
    }
}