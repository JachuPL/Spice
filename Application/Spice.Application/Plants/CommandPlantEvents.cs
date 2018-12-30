using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Application.Plants
{
    public class CommandPlantEvents : ICommandPlantEvents
    {
        private readonly IDatabaseService _database;
        private readonly IMapper _mapper;

        public CommandPlantEvents(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<Guid> Create(Guid plantId, CreatePlantEventModel model)
        {
            Plant plant = await _database.Plants.FindAsync(plantId);
            if (plant is null)
                throw new PlantDoesNotExistException(plantId);

            if (model.Occured < plant.Planted || DateTime.Now < model.Occured)
                throw new EventOccurenceDateBeforePlantDateOrInTheFutureException();

            Event @event = _mapper.Map<Event>(model);
            @event.Plant = plant;
            await _database.Events.AddAsync(@event);
            await _database.SaveAsync();
            return @event.Id;
        }

        public async Task<Event> Update(Guid plantId, UpdatePlantEventModel model)
        {
            Plant plant = await _database.Plants.Include(x => x.Events)
                .FirstOrDefaultAsync(x => x.Id == plantId);
            if (plant is null)
                throw new PlantDoesNotExistException(plantId);

            Event @event =
                plant.Events.FirstOrDefault(x => x.Id == model.Id);
            if (@event is null)
                return null;

            if (model.Occured < plant.Planted || DateTime.Now < model.Occured)
                throw new EventOccurenceDateBeforePlantDateOrInTheFutureException();

            _mapper.Map(model, @event);
            @event.Plant = plant;
            _database.Events.Update(@event);
            await _database.SaveAsync();
            return @event;
        }

        public async Task Delete(Guid plantId, Guid id)
        {
            Plant plant = await _database.Plants.Include(x => x.Events)
                .FirstOrDefaultAsync(x => x.Id == plantId);
            if (plant is null)
                return;

            Event @event = plant.Events.FirstOrDefault(x => x.Id == id);
            if (@event is null)
                return;

            plant.Events.Remove(@event);
            _database.Events.Remove(@event);
            await _database.SaveAsync();
        }
    }
}