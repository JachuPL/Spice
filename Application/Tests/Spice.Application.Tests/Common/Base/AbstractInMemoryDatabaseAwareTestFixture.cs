using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.AutoMapper;
using Spice.Domain;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using Spice.Persistence;
using System;

namespace Spice.Application.Tests.Common.Base
{
    internal abstract class AbstractInMemoryDatabaseAwareTestFixture
    {
        protected SpiceContext DatabaseContext { get; set; }
        protected IMapper Mapper { get; }

        protected AbstractInMemoryDatabaseAwareTestFixture()
        {
            Mapper = AutoMapperFactory.CreateMapper();
        }

        protected SpiceContext SetupInMemoryDatabase()
        {
            var ctxOptionsBuilder = new DbContextOptionsBuilder<SpiceContext>();
            ctxOptionsBuilder.UseInMemoryDatabase("TestSpiceDatabase");
            return new SpiceContext(ctxOptionsBuilder.Options);
        }

        protected Guid SeedDatabase(Field field)
        {
            DatabaseContext.Fields.Add(field);
            DatabaseContext.Save();

            return field.Id;
        }

        protected Guid SeedDatabase(Plant plant)
        {
            DatabaseContext.Plants.Add(plant);
            DatabaseContext.Save();

            return plant.Id;
        }

        protected Guid SeedDatabase(Domain.Species species)
        {
            DatabaseContext.Species.Add(species);
            DatabaseContext.Save();

            return species.Id;
        }

        protected Guid SeedDatabase(Nutrient nutrient)
        {
            DatabaseContext.Nutrients.Add(nutrient);
            DatabaseContext.Save();

            return nutrient.Id;
        }

        protected Guid SeedDatabase(AdministeredNutrient administeredNutrient)
        {
            DatabaseContext.AdministeredNutrients.Add(administeredNutrient);
            DatabaseContext.Save();

            return administeredNutrient.Id;
        }

        protected Guid SeedDatabase(Event @event)
        {
            DatabaseContext.Events.Add(@event);
            DatabaseContext.Save();

            return @event.Id;
        }
    }
}