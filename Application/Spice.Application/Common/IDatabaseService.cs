using Microsoft.EntityFrameworkCore;
using Spice.Domain;
using System.Threading.Tasks;

namespace Spice.Application.Common
{
    public interface IDatabaseService
    {
        DbSet<Plant> Plants { get; set; }

        int Save();

        Task<int> SaveAsync();
    }
}