using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Species.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Species
{
    public class QuerySpecies : IQuerySpecies
    {
        private readonly IDatabaseService _database;

        public QuerySpecies(IDatabaseService database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Domain.Plants.Species>> GetAll()
        {
            return await _database.Species.AsNoTracking().ToListAsync();
        }

        public async Task<Domain.Plants.Species> Get(Guid id)
        {
            return await _database.Species.FindAsync(id);
        }
    }
}