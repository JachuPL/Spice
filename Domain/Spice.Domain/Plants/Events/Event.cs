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
        public bool CreatedAutomatically { get; set; }

        protected Event()
        {
        }

        internal Event(Plant plant, EventType type, string description, bool createdAutomatically)
        {
            Plant = plant;
            Type = type;
            Description = description;
            CreatedAutomatically = createdAutomatically;
            Occured = DateTime.Now;
        }

        internal Event(Plant plant, EventType type, string description, bool createdAutomatically, DateTime occured) : this(plant, type, description, createdAutomatically)
        {
            Occured = occured;
        }
    }
}