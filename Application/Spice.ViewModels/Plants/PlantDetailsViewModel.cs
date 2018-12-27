using Spice.ViewModels.Fields;
using Spice.ViewModels.Species;
using System;

namespace Spice.ViewModels.Plants
{
    public class PlantDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SpeciesIndexViewModel Species { get; set; }
        public FieldIndexViewModel Field { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public DateTime Planted { get; set; }
        public PlantStateViewModel State { get; set; }
    }
}