using Spice.Application.Species.Models;
using Spice.Domain.Builders;
using System;

namespace Spice.Application.Tests.Species
{
    internal static class ModelFactory
    {
        public static Domain.Species DomainModel(string speciesName = "Pepper", string latinName = "Capsicum Annuum", string description = "Likes hot climate.")
        {
            return New.Species.WithName(speciesName).WithLatinName(latinName)
                      .WithDescription(description);
        }

        public static CreateSpeciesModel CreationModel(string speciesName = "Pepper", string latinName = "Capsicum Annuum", string description = "Likes hot climate.")
        {
            return new CreateSpeciesModel
            {
                Name = speciesName,
                LatinName = latinName,
                Description = description
            };
        }

        public static UpdateSpeciesModel UpdateModel(Guid? id = null, string speciesName = "Pepper", string latinName = "Capsicum Annuum", string description = "Likes hot climate.")
        {
            return new UpdateSpeciesModel
            {
                Id = id ?? Guid.NewGuid(),
                Name = speciesName,
                LatinName = latinName,
                Description = description,
            };
        }
    }
}