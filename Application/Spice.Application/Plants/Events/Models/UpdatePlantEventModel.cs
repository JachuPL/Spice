using Spice.Domain.Plants.Events;
using System;

namespace Spice.Application.Plants.Events.Models
{
    public class UpdatePlantEventModel
    {
        public Guid Id { get; set; }
        public EventType Type { get; set; }
        public string Description { get; set; }
        public DateTime Occured { get; set; }
    }
}