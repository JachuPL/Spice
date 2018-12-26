using System;
using System.Collections.Generic;

namespace Spice.Domain.Plants
{
    public class Species
    {
        public Guid Id { get; set; }
        public IEnumerable<Plant> Plants { get; set; }
    }
}