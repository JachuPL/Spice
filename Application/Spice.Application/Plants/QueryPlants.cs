using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Plants.Interfaces;
using Spice.Domain.Plants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Plants
{
    public class QueryPlants : IQueryPlants
    {
        private readonly IDatabaseService _database;

        public QueryPlants(IDatabaseService database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Plant>> GetAll()
        {
            return await _database.Plants
                .AsNoTracking()
                .Include(x => x.Species)
                .ToListAsync();
        }

        public async Task<Plant> Get(Guid id)
        {
            return await _database.Plants
                .AsNoTracking()
                .Include(x => x.Field)
                .Include(x => x.Species)
                .Include(x => x.AdministeredNutrients).ThenInclude(x => x.Nutrient)
                .Include(x => x.Events)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}