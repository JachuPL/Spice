using System;

namespace Spice.Application.Species.Exceptions
{
    public class SpeciesDoesNotExistException : ArgumentException
    {
        public SpeciesDoesNotExistException(Guid id) : base($"No species by id {id}")
        {
        }
    }
}