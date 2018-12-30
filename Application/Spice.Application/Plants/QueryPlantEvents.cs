using AutoMapper;
using Spice.Application.Common;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Domain.Plants.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Plants
{
    public class QueryPlantEvents : IQueryPlantEvents
    {
        private readonly IDatabaseService _database;
        private readonly IMapper _mapper;

        public QueryPlantEvents(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public Task<IEnumerable<Event>> GetByPlant(Guid plantId)
        {
            throw new NotImplementedException();
        }

        public Task<Event> Get(Guid plantId, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OccuredPlantEventsSummaryModel>> Sum(Guid plantId)
        {
            throw new NotImplementedException();
        }
    }
}