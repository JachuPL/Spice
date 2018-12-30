using System;

namespace Spice.ViewModels.Plants.Events
{
    public class PlantEventsIndexViewModel
    {
        public Guid Id { get; set; }
        public EventTypeViewModel Type { get; set; }
        public DateTime Occured { get; set; }
    }
}