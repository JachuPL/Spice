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

        public Task<IEnumerable<Field>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Field> Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}