using Spice.Application.Common.Exceptions;

namespace Spice.Application.Species.Exceptions
{
    public class SpeciesWithNameAlreadyExistsException : ResourceStateException
    {
        public SpeciesWithNameAlreadyExistsException(string name) : base($"Species with name {name} already exists.")
        {
        }
    }
}