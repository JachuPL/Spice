using System;

namespace Spice.ViewModels.Plants
{
    public class PlantIndexViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Specimen { get; set; }
        public PlantStateViewModel State { get; set; }
    }
}