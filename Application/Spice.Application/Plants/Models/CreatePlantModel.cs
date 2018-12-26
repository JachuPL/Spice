using Spice.Domain;
using System;

namespace Spice.Application.Plants.Models
{
    public class CreatePlantModel
    {
        public string Name { get; set; }
        public string Specimen { get; set; }
        public Guid FieldId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public DateTime Planted { get; set; }
        public PlantState State { get; set; }
    }
}