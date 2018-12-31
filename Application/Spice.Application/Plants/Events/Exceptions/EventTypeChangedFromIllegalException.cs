using Spice.Application.Common.Exceptions;
using Spice.Domain.Plants.Events;

namespace Spice.Application.Plants.Events.Exceptions
{
    public class EventTypeChangedFromIllegalException : ResourceStateException
    {
        public EventTypeChangedFromIllegalException(EventType type) : base($"Type cannot be changed from {type} to another one.")
        {
        }
    }
}