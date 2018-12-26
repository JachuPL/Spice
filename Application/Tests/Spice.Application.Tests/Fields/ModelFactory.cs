using Spice.Application.Fields.Models;
using Spice.Domain;
using System;

namespace Spice.Application.Tests.Fields
{
    public static class ModelFactory
    {
        public static CreateFieldModel CreationModel()
        {
            return new CreateFieldModel()
            {
                Name = "Field A",
                Description = "Random field description",
                Latitude = 52,
                Longtitude = 20
            };
        }

        public static UpdateFieldModel UpdateModel(Guid? id = null)
        {
            return new UpdateFieldModel()
            {
                Id = id ?? Guid.NewGuid(),
                Name = "Field A",
                Description = "Random field description",
                Latitude = 52,
                Longtitude = 20
            };
        }

        public static Field DomainModel(string fieldName = "Field A", double latitude = 52, double longtitude = 20)
        {
            return new Field()
            {
                Name = fieldName,
                Description = "Random field description",
                Latitude = latitude,
                Longtitude = longtitude
            };
        }
    }
}