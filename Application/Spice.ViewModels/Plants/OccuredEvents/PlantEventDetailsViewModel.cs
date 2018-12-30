using System;

namespace Spice.ViewModels.Plants.OccuredEvents
{
    public class PlantEventDetailsViewModel
    {
        public EventTypeViewModel Type { get; set; }
        public string Description { get; set; }
        public DateTime Occured { get; set; }
    }
}