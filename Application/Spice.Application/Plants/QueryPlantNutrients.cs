using Spice.Application.Common;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Domain.Plants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Plants
{
    public class QueryPlantNutrients : IQueryPlantNutrients
    {
        private readonly IDatabaseService _database;

        public QueryPlantNutrients(IDatabaseService database)
        {
            _database = database;
        }

        public Task<IEnumerable<AdministeredNutrient>> GetByPlant(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AdministeredNutrient> Get(Guid plantId, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdministeredPlantNutrientsSummaryModel>> Sum(Guid plantId)
        {
            throw new NotImplementedException();
        }
    }
}