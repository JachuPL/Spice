using Spice.Domain.Plants.Events;
using System;

namespace Spice.Application.Plants.Models
{
    public class CreatePlantEventModel
    {
        public EventType Type { get; set; }
        public string Description { get; set; }
        public DateTime Occured { get; set; }
    }
}