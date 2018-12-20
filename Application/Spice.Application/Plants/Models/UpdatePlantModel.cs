using Spice.Domain;
using System;

namespace Spice.Application.Plants.Models
{
    public class UpdatePlantModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Specimen { get; set; }
        public string FieldName { get; set; }
        public uint Row { get; set; }
        public uint Column { get; set; }
        public DateTime Planted { get; set; }
        public PlantState State { get; set; }
    }
}