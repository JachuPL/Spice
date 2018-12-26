using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.AutoMapper;
using Spice.Domain;
using Spice.Domain.Plants;
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
            using (var ctx = SetupInMemoryDatabase())
            {
                ctx.Fields.Add(field);
                ctx.Save();

                return field.Id;
            }
        }

        protected Guid SeedDatabase(Plant plant)
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                plant.Field = ctx.Fields.Find(plant.Field.Id);
                plant.Species = ctx.Species.Find(plant.Species.Id);
                ctx.Plants.Add(plant);
                ctx.Save();

                return plant.Id;
            }
        }

        protected Guid SeedDatabase(Domain.Plants.Species species)
        {
            using (var ctx = SetupInMemoryDatabase())
            {
                ctx.Species.Add(species);
                ctx.Save();

                return species.Id;
            }
        }
    }
}