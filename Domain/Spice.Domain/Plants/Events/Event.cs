using System;

namespace Spice.Domain.Plants.Events
{
    public class Event
    {
        public Guid Id { get; set; }
        public Plant Plant { get; set; }
        public EventType Type { get; set; }
        public string Description { get; set; }
        public DateTime Occured { get; set; }
    }
}