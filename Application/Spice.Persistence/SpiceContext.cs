using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Domain;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using Spice.Persistence.Configurations;
using System.Threading.Tasks;

namespace Spice.Persistence
{
    public class SpiceContext : DbContext, IDatabaseService
    {
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Nutrient> Nutrients { get; set; }
        public DbSet<AdministeredNutrient> AdministeredNutrients { get; set; }
        public DbSet<Event> Events { get; set; }

        public SpiceContext() : base()
        {
        }

        public SpiceContext(DbContextOptions<SpiceContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlantConfiguration());
            modelBuilder.ApplyConfiguration(new FieldConfiguration());
            modelBuilder.ApplyConfiguration(new SpeciesConfiguration());
            modelBuilder.ApplyConfiguration(new NutrientConfiguration());
            modelBuilder.ApplyConfiguration(new AdministeredNutrientConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public int Save()
        {
            return base.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}