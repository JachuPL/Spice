using Spice.Application.Nutrients.Models;
using Spice.Domain;
using System;

namespace Spice.Application.Tests.Nutrients
{
    internal static class ModelFactory
    {
        public static CreateNutrientModel CreationModel()
        {
            return new CreateNutrientModel()
            {
                Name = "Nutrient A",
                Description = "Random nutrient description",
                DosageUnits = "ml"
            };
        }

        public static UpdateNutrientModel UpdateModel(Guid? id = null)
        {
            return new UpdateNutrientModel()
            {
                Id = id ?? Guid.NewGuid(),
                Name = "Nutrient A",
                Description = "Random nutrient description",
                DosageUnits = "ml"
            };
        }

        public static Nutrient DomainModel(string nutrientName = "Nutrient A", string description = "Random nutrient description", string dosageUnits = "ml")
        {
            return new Nutrient()
            {
                Name = nutrientName,
                Description = description,
                DosageUnits = dosageUnits
            };
        }
    }
}