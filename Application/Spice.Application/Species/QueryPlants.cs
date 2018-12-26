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

        public Task<IEnumerable<Domain.Plants.Species>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Plants.Species> Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}