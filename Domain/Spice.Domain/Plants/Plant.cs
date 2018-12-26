﻿using System;

namespace Spice.Domain.Plants
{
    public class Plant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Species Species { get; set; }
        public Field Field { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public DateTime Planted { get; set; }
        public PlantState State { get; set; }
    }
}