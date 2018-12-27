using System;

namespace Spice.Domain
{
    public class Nutrient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DosageUnits { get; set; }
    }
}