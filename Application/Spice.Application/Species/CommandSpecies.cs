using AutoMapper;
using Spice.Application.Common;
using Spice.Application.Species.Interfaces;
using Spice.Application.Species.Models;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Species
{
    public class CommandSpecies : ICommandSpecies
    {
        private readonly IDatabaseService _database;
        private readonly IMapper _mapper;

        public CommandSpecies(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public Task<Guid> Create(CreateSpeciesModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Plants.Species> Update(UpdateSpeciesModel ignored)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}