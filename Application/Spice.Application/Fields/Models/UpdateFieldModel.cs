using System;

namespace Spice.Application.Fields.Models
{
    public class UpdateFieldModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
    }
}