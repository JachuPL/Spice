using System;

namespace Spice.Application.Plants.Events.Exceptions
{
    public class EventOccurenceDateBeforePlantDateOrInTheFutureException : InvalidOperationException
    {
        public EventOccurenceDateBeforePlantDateOrInTheFutureException() : base("Event occurence date cannot be earlier than plant date or in the future.")
        {
        }
    }
}