using System;

namespace Spice.Application.Common.Exceptions
{
    public abstract class ResourceNotFoundException : Exception
    {
        protected ResourceNotFoundException(Type type, Guid id) : base($"No {type} by id {id}.")
        {
        }
    }
}