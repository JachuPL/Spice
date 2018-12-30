using Spice.Domain.Plants.Events;

namespace Spice.Application.Plants.Events.Models
{
    public class OccuredPlantEventsSummaryModel
    {
        public EventType Type { get; set; }
        public int TotalCount { get; set; }
    }
}