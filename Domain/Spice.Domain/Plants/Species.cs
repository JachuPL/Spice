using System;
using System.Collections.Generic;

namespace Spice.Domain.Plants
{
    public class Species
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Description { get; set; }
        public IEnumerable<Plant> Plants { get; set; }
    }
}