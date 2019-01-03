using Spice.Domain.Plants.Events;
using System;
using System.Collections.Generic;

namespace Spice.Domain.Plants
{
    public class Plant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Species Species { get; set; }
        public Field Field { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public DateTime Planted { get; set; }
        public PlantState State { get; set; }
        public ICollection<AdministeredNutrient> AdministeredNutrients { get; set; }
        public ICollection<Event> Events { get; set; }

        protected Plant()
        {
            AdministeredNutrients = new List<AdministeredNutrient>();
            Events = new List<Event>();
        }

        public Plant(string name, Species species, Field field, int fieldRow, int fieldColumn) : this()
        {
            Column = fieldColumn;
            Row = fieldRow;
            Field = field;
            Species = species;
            Name = name;
            Planted = DateTime.Now;
            State = PlantState.Healthy;
            AddCreationEvent();
        }

        private void AddCreationEvent()
        {
            Event plantCreatedEvent = new Event(this, EventType.Start,
                $"{Name} was planted on field {Field.Name}. (Generated automatically)");
            Events.Add(plantCreatedEvent);
        }

        public void ChangeField(Field newField)
        {
            if (Field != newField)
            {
                Field = newField;
                AddFieldChangeEvent(newField);
            }
        }

        private void AddFieldChangeEvent(Field newField)
        {
            Event fieldChangedEvent = new Event(this, EventType.Moving,
                $"{Name} was moved to field {newField.Name}. (Generated automatically)");
            Events.Add(fieldChangedEvent);
        }

        public Event AddEvent(EventType type, string description)
        {
            return AddEvent(type, description, DateTime.Now);
        }

        public Event AddEvent(EventType type, string description, DateTime occured)
        {
            Event newEvent = new Event(this, type, description, occured);
            Events.Add(newEvent);
            return newEvent;
        }

        public AdministeredNutrient AdministerNutrient(Nutrient nutrient, double amount, DateTime date, bool createEvent = false)
        {
            AdministeredNutrient administeredNutrient = new AdministeredNutrient(this, nutrient, amount, date);
            AdministeredNutrients.Add(administeredNutrient);
            if (createEvent)
            {
                Event @event = AddEvent(EventType.Nutrition,
                    $"Given {amount} {nutrient.DosageUnits} of {nutrient.Name} to {Name}. (Generated automatically)",
                    date);
                Events.Add(@event);
            }

            return administeredNutrient;
        }
    }
}