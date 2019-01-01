using Spice.Domain.Plants.Events;

namespace Spice.Application.Plants.Events.Models.Summary
{
    public class PlantEventOccurenceCountModel
    {
        public EventType Type { get; set; }
        public int TotalCount { get; set; }
    }
}