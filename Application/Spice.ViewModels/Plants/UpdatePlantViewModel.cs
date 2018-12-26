using System;

namespace Spice.ViewModels.Plants
{
    public class UpdatePlantViewModel
    {
        public string Name { get; set; }
        public string Specimen { get; set; }
        public string FieldName { get; set; }
        public uint Row { get; set; }
        public uint Column { get; set; }
        public DateTime Planted { get; set; }
        public PlantStateViewModel State { get; set; }
    }
}