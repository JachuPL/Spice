using System;

namespace Spice.ViewModels.Plants.OccuredEvents
{
    public class PlantEventsIndexViewModel
    {
        public Guid Id { get; set; }
        public EventTypeViewModel Type { get; set; }
        public DateTime Occured { get; set; }
    }
}