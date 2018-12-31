using Spice.Application.Common.Exceptions;
using Spice.Domain.Plants.Events;

namespace Spice.Application.Plants.Events.Exceptions
{
    public class EventTypeChangedToIllegalException : ResourceStateException
    {
        public EventTypeChangedToIllegalException(EventType type) : base($"Event type cannot be changed to {type}.")
        {
        }
    }
}