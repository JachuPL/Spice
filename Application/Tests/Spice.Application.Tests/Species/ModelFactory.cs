using Spice.Application.Species.Models;
using System;

namespace Spice.Application.Tests.Species
{
    public class ModelFactory
    {
        public static Domain.Plants.Species DomainModel(string speciesName = "Pepper")
        {
            return new Domain.Plants.Species()
            {
                Name = speciesName,
                LatinName = "Capsicum annuum",
                Description = "Likes hot climate. Hates overwatering."
            };
        }

        public static CreateSpeciesModel CreationModel()
        {
            return new CreateSpeciesModel()
            {
                Name = "Pepper",
                LatinName = "Capsicum annuum",
                Description = "Random pepper description"
            };
        }

        public static UpdateSpeciesModel UpdateModel(Guid? id = null)
        {
            return new UpdateSpeciesModel()
            {
                Id = id ?? Guid.NewGuid(),
                Name = "Pepper",
                LatinName = "Capsicum annuum",
                Description = "Random pepper description",
            };
        }
    }
}