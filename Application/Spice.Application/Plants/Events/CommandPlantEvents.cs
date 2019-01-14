using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Plants.Events.Exceptions;
using Spice.Application.Plants.Events.Interfaces;
using Spice.Application.Plants.Events.Models;
using Spice.Application.Plants.Exceptions;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Application.Plants.Events
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
            {
                throw new PlantNotFoundException(plantId);
            }

            if (EventOccuredBeforePlantedOrInTheFuture(plant, model.Occured))
            {
                throw new EventOccurenceDateBeforePlantDateOrInTheFutureException();
            }

            if (model.Type.IsCreationRestricted())
            {
                throw new EventTypeIsCreationRestrictedException(model.Type);
            }

            Event @event = plant.AddEvent(model.Type, model.Description, model.Occured, false);
            _database.Plants.Update(plant);
            await _database.SaveAsync();
            return @event.Id;
        }

        private static bool EventOccuredBeforePlantedOrInTheFuture(Plant plant, DateTime occurence)
        {
            return (occurence < plant.Planted) || (DateTime.Now < occurence);
        }

        public async Task<Event> Update(Guid plantId, UpdatePlantEventModel model)
        {
            Plant plant = await _database.Plants.Include(x => x.Events)
                .FirstOrDefaultAsync(x => x.Id == plantId);
            if (plant is null)
            {
                throw new PlantNotFoundException(plantId);
            }

            Event @event = plant.Events.FirstOrDefault(x => x.Id == model.Id);
            if (@event is null)
            {
                return null;
            }

            if (EventOccuredBeforePlantedOrInTheFuture(plant, model.Occured))
            {
                throw new EventOccurenceDateBeforePlantDateOrInTheFutureException();
            }

            if (@event.Type != model.Type)
            {
                if (!@event.Type.IsChangeable())
                {
                    throw new EventTypeChangedFromIllegalException(@event.Type);
                }

                if (!model.Type.IsChangeable())
                {
                    throw new EventTypeChangedToIllegalException(model.Type);
                }
            }

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
            {
                return;
            }

            Event @event = plant.Events.FirstOrDefault(x => x.Id == id);
            if (@event is null)
            {
                return;
            }

            plant.Events.Remove(@event);
            _database.Events.Remove(@event);
            await _database.SaveAsync();
        }
    }
}