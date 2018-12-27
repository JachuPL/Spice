using System;

namespace Spice.Application.Species.Models
{
    public class UpdateSpeciesModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Description { get; set; }
    }
}