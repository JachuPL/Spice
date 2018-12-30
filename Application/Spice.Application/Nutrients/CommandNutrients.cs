using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Spice.Application.Common;
using Spice.Application.Nutrients.Exceptions;
using Spice.Application.Nutrients.Interfaces;
using Spice.Application.Nutrients.Models;
using Spice.Domain;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Nutrients
{
    public class CommandNutrients : ICommandNutrients
    {
        private readonly IDatabaseService _database;
        private readonly IMapper _mapper;

        public CommandNutrients(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<Guid> Create(CreateNutrientModel model)
        {
            if (await _database.Nutrients.AnyAsync(x => x.Name == model.Name))
                throw new NutrientWithNameAlreadyExistsException(model.Name);

            Nutrient nutrient = _mapper.Map<Nutrient>(model);
            await _database.Nutrients.AddAsync(nutrient);
            await _database.SaveAsync();
            return nutrient.Id;
        }

        public async Task<Nutrient> Update(UpdateNutrientModel model)
        {
            Nutrient nutrient = await _database.Nutrients
                .Include(x => x.AdministeredToPlants)
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            if (nutrient is null)
                return null;

            if (nutrient.AdministeredToPlants.Any())
                throw new NutrientAdministeredToAPlantException();

            if (await _database.Nutrients.AnyAsync(x => x.Name == model.Name && x.Id != model.Id))
                throw new NutrientWithNameAlreadyExistsException(model.Name);

            _mapper.Map(model, nutrient);
            _database.Nutrients.Update(nutrient);
            await _database.SaveAsync();
            return nutrient;
        }

        public async Task Delete(Guid id)
        {
            Nutrient nutrient = await _database.Nutrients.FindAsync(id);
            if (nutrient is null)
                return;

            _database.Nutrients.Remove(nutrient);
            await _database.SaveAsync();
        }
    }
}