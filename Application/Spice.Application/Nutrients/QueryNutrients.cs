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

        public Task<IEnumerable<Nutrient>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Nutrient> Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}