using Spice.ViewModels.Fields;
using Spice.ViewModels.Plants.AdministeredNutrients;
using Spice.ViewModels.Plants.OccuredEvents;
using Spice.ViewModels.Species;
using System;
using System.Collections.Generic;

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
        public IEnumerable<AdministeredNutrientsIndexViewModel> Nutrients { get; set; }
        public IEnumerable<PlantEventsIndexViewModel> Events { get; set; }
    }
}