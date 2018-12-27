using Spice.ViewModels.Plants;
using System;
using System.Collections.Generic;

namespace Spice.ViewModels.Species
{
    public class SpeciesDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Description { get; set; }
        public IEnumerable<PlantIndexViewModel> Plants { get; set; }
    }
}