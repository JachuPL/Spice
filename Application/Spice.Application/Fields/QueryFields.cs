using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Fields.Interfaces;
using Spice.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Fields
{
    public class QueryFields : IQueryFields
    {
        private readonly IDatabaseService _database;

        public QueryFields(IDatabaseService database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Field>> GetAll()
        {
            return await _database.Fields.AsNoTracking().ToListAsync();
        }

        public async Task<Field> Get(Guid id)
        {
            return await _database.Fields
                .Include(x => x.Plants)
                .ThenInclude(x => x.Species).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}