using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Nutrients.Interfaces;
using Spice.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Nutrients
{
    public class QueryNutrients : IQueryNutrients
    {
        private readonly IDatabaseService _database;

        public QueryNutrients(IDatabaseService database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Nutrient>> GetAll()
        {
            return await _database.Nutrients.AsNoTracking().ToListAsync();
        }

        public async Task<Nutrient> Get(Guid id)
        {
            return await _database.Nutrients.FindAsync(id);
        }
    }
}