using System;

namespace Spice.ViewModels.Plants.Events.Summary
{
    public class PlantEventOccurenceCountViewModel
    {
        public EventTypeViewModel Type { get; set; }
        public int TotalCount { get; set; }
        public DateTime FirstOccurence { get; set; }
        public DateTime LastOccurence { get; set; }
    }
}