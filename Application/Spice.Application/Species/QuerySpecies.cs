using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Species.Interfaces;
using Spice.Application.Species.Models;
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

        public async Task<IEnumerable<Domain.Species>> GetAll()
        {
            return await _database.Species.AsNoTracking().ToListAsync();
        }

        public async Task<Domain.Species> Get(Guid id)
        {
            return await _database.Species
                .Include(x => x.Plants)
                .ThenInclude(x => x.Field).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<IEnumerable<SpeciesNutritionSummaryModel>> Summary(Guid id, DateTime? fromDate = null, DateTime? toDate = null)
        {
            throw new NotImplementedException();
        }
    }
}