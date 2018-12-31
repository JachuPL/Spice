using Spice.Application.Common.Exceptions;

namespace Spice.Application.Plants.Events.Exceptions
{
    public class EventOccurenceDateBeforePlantDateOrInTheFutureException : ResourceStateException
    {
        public EventOccurenceDateBeforePlantDateOrInTheFutureException() : base("Event occurence date cannot be earlier than plant date or in the future.")
        {
        }
    }
}