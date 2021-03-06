﻿using Spice.Domain.Plants;
using System;

namespace Spice.Application.Plants.Models
{
    public class UpdatePlantModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SpeciesId { get; set; }
        public Guid FieldId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public DateTime Planted { get; set; }
        public PlantState State { get; set; }
    }
}