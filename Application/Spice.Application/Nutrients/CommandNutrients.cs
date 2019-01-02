using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Nutrients.Exceptions;
using Spice.Application.Nutrients.Interfaces;
using Spice.Application.Nutrients.Models;
using Spice.Domain;
using System;
using System.Linq;
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
            IQueryable<Nutrient> existingNutrientsWithName = from existingNutrient in _database.Nutrients
                                                             where existingNutrient.Name == model.Name
                                                             select existingNutrient;
            if (await existingNutrientsWithName.AnyAsync())
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

            IQueryable<Nutrient> existingNutrientsWithName = from existingNutrient in _database.Nutrients
                                                             where existingNutrient.Name == model.Name && existingNutrient.Id != model.Id
                                                             select existingNutrient;
            if (await existingNutrientsWithName.AnyAsync())
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