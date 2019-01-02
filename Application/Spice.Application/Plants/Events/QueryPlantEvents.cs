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
            Plant plant = await _database.Plants
                .AsNoTracking()
                .Include(x => x.Events)
                .FirstOrDefaultAsync(x => x.Id == plantId);

            return plant?.Events;
        }

        public async Task<Event> Get(Guid plantId, Guid id)
        {
            Plant plant = await _database.Plants
                .AsNoTracking()
                .Include(x => x.Events)
                .FirstOrDefaultAsync(x => x.Id == plantId);

            return plant?.Events.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<PlantEventOccurenceCountModel>> Summary(Guid plantId, DateTime? startDate = null, DateTime? endDate = null)
        {
            Plant existingPlant = await _database.Plants
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == plantId);
            if (existingPlant is null)
                return null;

            IQueryable<Event> occuredEvents =
                from events in _database.Events.AsNoTracking()
                join plant in _database.Plants on events.Plant.Id equals plant.Id
                where plant.Id == existingPlant.Id
                select events;
            if (startDate.HasValue)
                occuredEvents = occuredEvents.Where(x => startDate.Value <= x.Occured);

            if (endDate.HasValue)
                occuredEvents = occuredEvents.Where(x => x.Occured <= endDate.Value);

            IQueryable<PlantEventOccurenceCountModel> occuredEventsByType =
                from occuredEvent in occuredEvents
                group occuredEvent by occuredEvent.Type
                into occuredEventType
                select new PlantEventOccurenceCountModel()
                {
                    Type = occuredEventType.Key,
                    TotalCount = occuredEventType.Count(),
                    FirstOccurence = occuredEventType.Min(z => z.Occured),
                    LastOccurence = occuredEventType.Max(z => z.Occured)
                };

            return await occuredEventsByType.ToListAsync();
        }
    }
}