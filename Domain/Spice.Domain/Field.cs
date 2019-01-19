using Spice.Domain.Plants;
using System;
using System.Collections.Generic;

namespace Spice.Domain
{
    public class Field
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public ICollection<Plant> Plants { get; set; }

        private Field()
        {
            Plants = new List<Plant>();
        }

        internal Field(string name, string description, double latitude, double longtitude) : this()
        {
            Name = name;
            Description = description;
            Latitude = latitude;
            Longtitude = longtitude;
        }
    }
}