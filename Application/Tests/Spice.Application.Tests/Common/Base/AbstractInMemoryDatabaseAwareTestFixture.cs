using Microsoft.EntityFrameworkCore;
using Spice.Domain;
using Spice.Persistence;
using System;

namespace Spice.Application.Tests.Common.Base
{
    internal abstract class AbstractInMemoryDatabaseAwareTestFixture
    {
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
                ctx.Plants.Add(plant);
                ctx.Save();

                return plant.Id;
            }
        }
    }
}