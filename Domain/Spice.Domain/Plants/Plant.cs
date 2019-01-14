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

        public Plant(string name, Species species, Field field, int fieldRow, int fieldColumn, PlantState state) :
            this(name, species, field, fieldRow, fieldColumn)
        {
            State = state;
        }

        private void AddCreationEvent()
        {
            Event plantCreatedEvent =
                new Event(this, EventType.StartedTracking, $"{Name} was added to Spice tracklog.", true);
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
                                                $"{Name} was moved to field {newField.Name}. (Generated automatically)", true);
            Events.Add(fieldChangedEvent);
        }

        public Event AddEvent(EventType type, string description, bool createdAutomatically)
        {
            return AddEvent(type, description, DateTime.Now, createdAutomatically);
        }

        public Event AddEvent(EventType type, string description, DateTime occured, bool createdAutomatically)
        {
            Event newEvent = new Event(this, type, description, createdAutomatically, occured);
            Events.Add(newEvent);
            return newEvent;
        }

        public AdministeredNutrient AdministerNutrient(Nutrient nutrient, double amount, DateTime date,
                                                       bool createEvent = false)
        {
            AdministeredNutrient administeredNutrient = new AdministeredNutrient(this, nutrient, amount, date);
            AdministeredNutrients.Add(administeredNutrient);
            if (createEvent)
            {
                AddEvent(EventType.Nutrition,
                         $"Given {amount} {nutrient.DosageUnits} of {nutrient.Name} to {Name}. (Generated automatically)",
                         date, true);
            }

            return administeredNutrient;
        }

        public void UpdateState(PlantState state)
        {
            State = state;
            switch (State)
            {
                case PlantState.Sprouting:
                    AddEvent(EventType.Sprouting, $"{Name} is sprouting.", true);
                    break;

                case PlantState.Flowering:
                    AddEvent(EventType.Growth, $"{Name} has its first flowers.", true);
                    break;

                case PlantState.Fruiting:
                    AddEvent(EventType.Growth, $"{Name} has its first fruits!", true);
                    break;
            }
        }
    }
}