using Spice.Domain.Plants;
using System;
using System.Collections.Generic;

namespace Spice.Domain
{
    public class Field
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public ICollection<Plant> Plants { get; set; } = new List<Plant>();
    }
}