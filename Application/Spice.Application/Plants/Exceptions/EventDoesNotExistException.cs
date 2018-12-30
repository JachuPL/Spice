using System;

namespace Spice.Application.Plants.Exceptions
{
    public class EventDoesNotExistException : ArgumentException
    {
        public EventDoesNotExistException(Guid id) : base($"No event by id {id}")
        {
        }
    }
}