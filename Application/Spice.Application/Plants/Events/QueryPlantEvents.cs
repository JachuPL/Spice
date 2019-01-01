using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Plants.Events.Interfaces;
using Spice.Application.Plants.Events.Models.Summary;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Application.Plants.Events
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

            return plant?.Events;
        }

        public async Task<Event> Get(Guid plantId, Guid id)
        {
            Plant plant = await _database.Plants.Include(x => x.Events)
                .FirstOrDefaultAsync(x => x.Id == plantId);

            return plant?.Events.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<PlantEventOccurenceCountModel>> Summary(Guid plantId, DateTime? startDate = null, DateTime? endDate = null)
        {
            Plant plant = await _database.Plants.Include(x => x.Events)
                .FirstOrDefaultAsync(x => x.Id == plantId);

            if (plant is null)
                return null;

            IEnumerable<Event> events = plant.Events;

            if (startDate.HasValue)
                events = events.Where(x => startDate.Value <= x.Occured);

            if (endDate.HasValue)
                events = events.Where(x => x.Occured <= endDate.Value);

            return events.GroupBy(x => x.Type)
                .Select(x => new PlantEventOccurenceCountModel()
                {
                    Type = x.Key,
                    TotalCount = x.Count(),
                    FirstOccurence = x.Min(z => z.Occured),
                    LastOccurence = x.Max(z => z.Occured)
                }).ToList();
        }
    }
}