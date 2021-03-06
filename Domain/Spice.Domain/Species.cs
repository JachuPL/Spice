﻿using Spice.Domain.Plants;
using System;
using System.Collections.Generic;

namespace Spice.Domain
{
    public class Species
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Description { get; set; }
        public ICollection<Plant> Plants { get; set; } = new List<Plant>();
    }
}