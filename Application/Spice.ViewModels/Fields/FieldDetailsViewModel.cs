using Spice.ViewModels.Plants;
using System.Collections.Generic;

namespace Spice.ViewModels.Fields
{
    public class FieldDetailsViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public IEnumerable<PlantIndexViewModel> Plants { get; set; }
    }
}