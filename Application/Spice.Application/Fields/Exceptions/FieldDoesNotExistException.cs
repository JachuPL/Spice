using System;

namespace Spice.Application.Fields.Exceptions
{
    public class FieldDoesNotExistException : ArgumentException
    {
        public FieldDoesNotExistException(Guid id) : base($"There is not field with id {id}")
        {
        }
    }
}