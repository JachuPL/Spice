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

        protected Event()
        {
        }

        public Event(Plant plant, EventType type, string description)
        {
            Plant = plant;
            Type = type;
            Description = description;
            Occured = DateTime.Now;
        }

        public Event(Plant plant, EventType type, string description, DateTime occured) : this(plant, type, description)
        {
            Occured = occured;
        }
    }
}