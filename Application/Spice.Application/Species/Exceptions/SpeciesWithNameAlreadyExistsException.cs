using System;

namespace Spice.Application.Species.Exceptions
{
    public class SpeciesWithNameAlreadyExistsException : ArgumentException
    {
        public SpeciesWithNameAlreadyExistsException(string name) : base($"Species with name {name} already exists.")
        {
        }
    }
}