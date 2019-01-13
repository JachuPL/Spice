using Spice.Application.Fields.Models;
using Spice.Domain;
using Spice.Domain.Builders;
using System;

namespace Spice.Application.Tests.Fields
{
    internal static class ModelFactory
    {
        public static CreateFieldModel CreationModel(string fieldName = "Field A", string description = "Random field description", double latitude = 52, double longtitude = 20)
        {
            return new CreateFieldModel
            {
                Name = fieldName,
                Description = description,
                Latitude = latitude,
                Longtitude = longtitude
            };
        }

        public static UpdateFieldModel UpdateModel(Guid? id = null, string fieldName = "Field A", string description = "Random field description", double latitude = 52, double longtitude = 20)
        {
            return new UpdateFieldModel
            {
                Id = id ?? Guid.NewGuid(),
                Name = fieldName,
                Description = description,
                Latitude = latitude,
                Longtitude = longtitude
            };
        }

        public static Field DomainModel(string fieldName = "Field A", string description = "Random field description", double latitude = 52, double longtitude = 20)
        {
            return New.Field.WithName(fieldName)
                                     .WithDescription(description)
                                     .OnLatitude(latitude)
                                     .OnLongtitude(longtitude);
        }
    }
}