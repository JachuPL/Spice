using Microsoft.EntityFrameworkCore;
using Spice.Domain;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using System.Threading.Tasks;

namespace Spice.Application.Common
{
    public interface IDatabaseService
    {
        DbSet<Plant> Plants { get; }
        DbSet<Field> Fields { get; }
        DbSet<Domain.Species> Species { get; }
        DbSet<Nutrient> Nutrients { get; }
        DbSet<AdministeredNutrient> AdministeredNutrients { get; }
        DbSet<Event> Events { get; }

        int Save();

        Task<int> SaveAsync();
    }
}