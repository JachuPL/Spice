using AutoMapper;
using Spice.Application.Common;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Domain.Plants.Events;
using System;
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

        public Task<Guid> Create(Guid plantId, CreatePlantEventModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Event> Update(Guid plantId, UpdatePlantEventModel model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid plantId, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}