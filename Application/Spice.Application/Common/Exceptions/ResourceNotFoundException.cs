using System;

namespace Spice.Application.Common.Exceptions
{
    public abstract class ResourceNotFoundException : Exception
    {
        protected ResourceNotFoundException(Type type, Guid id) : base($"{type.Name} with id {id} could not be found.")
        {
        }
    }
}