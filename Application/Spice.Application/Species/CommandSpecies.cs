using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Species.Exceptions;
using Spice.Application.Species.Interfaces;
using Spice.Application.Species.Models;
using System;
using System.Linq;
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

        public async Task<Guid> Create(CreateSpeciesModel model)
        {
            IQueryable<Domain.Species> anyWithNameAndLatinName =
                from existingSpeciesWithNameAndLatinName in _database.Species
                where existingSpeciesWithNameAndLatinName.Name == model.Name &&
                      existingSpeciesWithNameAndLatinName.LatinName == model.LatinName
                select existingSpeciesWithNameAndLatinName;

            if (await anyWithNameAndLatinName.AnyAsync())
                throw new SpeciesWithNameAlreadyExistsException(model.Name);

            Domain.Species species = _mapper.Map<Domain.Species>(model);
            await _database.Species.AddAsync(species);
            await _database.SaveAsync();
            return species.Id;
        }

        public async Task<Domain.Species> Update(UpdateSpeciesModel model)
        {
            Domain.Species species = await _database.Species.FindAsync(model.Id);
            if (species is null)
                return null;

            IQueryable<Domain.Species> anyWithNameAndLatinName =
                from existingSpeciesWithNameAndLatinName in _database.Species
                where existingSpeciesWithNameAndLatinName.Name == model.Name &&
                      existingSpeciesWithNameAndLatinName.LatinName == model.LatinName &&
                      existingSpeciesWithNameAndLatinName.Id != model.Id
                select existingSpeciesWithNameAndLatinName;
            if (await anyWithNameAndLatinName.AnyAsync())
                throw new SpeciesWithNameAlreadyExistsException(model.Name);

            _mapper.Map(model, species);
            _database.Species.Update(species);
            await _database.SaveAsync();
            return species;
        }

        public async Task Delete(Guid id)
        {
            Domain.Species species = await _database.Species.FindAsync(id);
            if (species is null)
                return;

            _database.Species.Remove(species);
            await _database.SaveAsync();
        }
    }
}