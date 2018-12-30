using System;

namespace Spice.Application.Plants.Exceptions
{
    public class EventOccurenceDateBeforePlantDateException : InvalidOperationException
    {
        public EventOccurenceDateBeforePlantDateException() : base("Event occurence date cannot be earlier than plant date")
        {
        }
    }
}