using Spice.Application.Plants.Nutrients.Models;
using Spice.Domain;
using Spice.Domain.Plants;
using System;

namespace Spice.Application.Tests.Plants.Nutrients
{
    public static class ModelFactory
    {
        public static CreateAdministeredNutrientModel CreationModel(Guid? nutrientId = null, DateTime? administeredDate = null)
        {
            return new CreateAdministeredNutrientModel()
            {
                Date = administeredDate ?? DateTime.Now,
                NutrientId = nutrientId ?? Guid.NewGuid(),
                Amount = 1.0
            };
        }

        public static UpdateAdministeredNutrientModel UpdateModel(Guid? id = null, Guid? nutrientId = null, DateTime? administeredDate = null)
        {
            return new UpdateAdministeredNutrientModel()
            {
                Id = id ?? Guid.NewGuid(),
                Date = administeredDate ?? DateTime.Now,
                NutrientId = nutrientId ?? Guid.NewGuid(),
                Amount = 1.0
            };
        }

        public static AdministeredNutrient DomainModel(Nutrient nutrient = null, Plant plant = null)
        {
            return new AdministeredNutrient()
            {
                Nutrient = nutrient ?? Tests.Nutrients.ModelFactory.DomainModel(),
                Plant = plant ?? Plants.ModelFactory.DomainModel(),
                Amount = 1.0,
                Date = DateTime.Now
            };
        }
    }
}