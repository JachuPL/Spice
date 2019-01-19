using Spice.Application.Nutrients.Models;
using Spice.Domain;
using Spice.Domain.Builders;
using System;

namespace Spice.Application.Tests.Nutrients
{
    internal static class ModelFactory
    {
        public static CreateNutrientModel CreationModel(string nutrientName = "Nutrient A", string description = "Random nutrient description", string dosageUnits = "ml")
        {
            return new CreateNutrientModel
            {
                Name = nutrientName,
                Description = description,
                DosageUnits = dosageUnits
            };
        }

        public static UpdateNutrientModel UpdateModel(Guid? id = null, string nutrientName = "Nutrient A", string description = "Random nutrient description", string dosageUnits = "ml")
        {
            return new UpdateNutrientModel
            {
                Id = id ?? Guid.NewGuid(),
                Name = nutrientName,
                Description = description,
                DosageUnits = dosageUnits
            };
        }

        public static Nutrient DomainModel(string nutrientName = "Nutrient A", string description = "Random nutrient description", string dosageUnits = "ml")
        {
            return New.Nutrient.WithName(nutrientName).WithDescription(description).WithDosageUnits(dosageUnits);
        }
    }
}