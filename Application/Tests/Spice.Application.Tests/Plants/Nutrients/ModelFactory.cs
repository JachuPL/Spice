using Spice.Application.Plants.Nutrients.Models;
using Spice.Domain;
using Spice.Domain.Plants;
using System;

namespace Spice.Application.Tests.Plants.Nutrients
{
    internal static class ModelFactory
    {
        public static CreateAdministeredNutrientModel CreationModel(Guid? nutrientId = null, DateTime? administeredDate = null, bool createEvent = false)
        {
            return new CreateAdministeredNutrientModel()
            {
                Date = administeredDate ?? DateTime.Now,
                NutrientId = nutrientId ?? Guid.NewGuid(),
                Amount = 1.0,
                CreateEvent = createEvent
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

        public static AdministeredNutrient DomainModel(Nutrient nutrient = null, Plant plant = null, DateTime? date = null, bool createEvent = false)
        {
            Plant nutritionedPlant = plant ?? Plants.ModelFactory.DomainModel();
            return nutritionedPlant.AdministerNutrient(
                nutrient ?? Tests.Nutrients.ModelFactory.DomainModel(),
                1.0,
                date ?? DateTime.Now,
                createEvent);
        }
    }
}