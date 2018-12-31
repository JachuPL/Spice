using Spice.Application.Common.Exceptions;
using Spice.Domain.Plants.Events;

namespace Spice.Application.Plants.Events.Exceptions
{
    public class EventTypeIsCreationRestrictedException : ResourceStateException
    {
        public EventTypeIsCreationRestrictedException(EventType type) : base($"Cannot create event with type {type}.")
        {
        }
    }
}