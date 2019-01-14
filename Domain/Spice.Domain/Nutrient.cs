using Spice.Domain.Plants;
using System;
using System.Collections.Generic;

namespace Spice.Domain
{
    public class Nutrient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DosageUnits { get; set; }
        public ICollection<AdministeredNutrient> AdministeredToPlants { get; set; }

        private Nutrient()
        {
            AdministeredToPlants = new List<AdministeredNutrient>();
        }

        internal Nutrient(string name, string description, string dosageUnits) : this()
        {
            Name = name;
            Description = description;
            DosageUnits = dosageUnits;
        }
    }
}