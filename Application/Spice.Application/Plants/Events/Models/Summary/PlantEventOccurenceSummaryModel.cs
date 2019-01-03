using Spice.Domain.Plants.Events;
using System;

namespace Spice.Application.Plants.Events.Models.Summary
{
    public class PlantEventOccurenceSummaryModel
    {
        public EventType Type { get; set; }
        public int TotalCount { get; set; }
        public DateTime FirstOccurence { get; set; }
        public DateTime LastOccurence { get; set; }
    }
}