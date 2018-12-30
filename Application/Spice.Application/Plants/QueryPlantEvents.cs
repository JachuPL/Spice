using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Event>> GetByPlant(Guid plantId)
        {
            Plant plant = await _database.Plants.Include(x => x.Events)
                .FirstOrDefaultAsync(x => x.Id == plantId);
            if (plant is null)
                throw new PlantDoesNotExistException(plantId);

            return plant.Events;
        }

        public async Task<Event> Get(Guid plantId, Guid id)
        {
            Plant plant = await _database.Plants.Include(x => x.Events)
                .FirstOrDefaultAsync(x => x.Id == plantId);
            if (plant is null)
                throw new PlantDoesNotExistException(plantId);

            return plant.Events.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<OccuredPlantEventsSummaryModel>> Sum(Guid plantId)
        {
            Plant plant = await _database.Plants.Include(x => x.Events)
                .FirstOrDefaultAsync(x => x.Id == plantId);

            if (plant is null)
                throw new PlantDoesNotExistException(plantId);

            return plant.Events.GroupBy(x => x.Type)
                .Select(x => new OccuredPlantEventsSummaryModel()
                {
                    Type = x.Key,
                    TotalCount = x.Sum(z => 1)
                }).ToList();
        }
    }
}