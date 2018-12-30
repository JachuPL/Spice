using System;

namespace Spice.ViewModels.Plants.Events
{
    public class PlantEventDetailsViewModel
    {
        public Guid Id { get; set; }
        public EventTypeViewModel Type { get; set; }
        public string Description { get; set; }
        public DateTime Occured { get; set; }
    }
}