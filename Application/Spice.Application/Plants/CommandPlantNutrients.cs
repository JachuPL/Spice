using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Nutrients.Exceptions;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Domain;
using Spice.Domain.Plants;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Application.Plants
{
    public class CommandPlantNutrients : ICommandPlantNutrients
    {
        private readonly IDatabaseService _database;
        private readonly IMapper _mapper;

        public CommandPlantNutrients(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<Guid> Create(Guid plantId, CreateAdministeredNutrientModel model)
        {
            Plant plant = await _database.Plants.FindAsync(plantId);
            if (plant is null)
                throw new PlantDoesNotExistException(plantId);

            Nutrient nutrient = await _database.Nutrients.FindAsync(model.NutrientId);
            if (nutrient is null)
                throw new NutrientDoesNotExistException(model.NutrientId);

            if (model.Date < plant.Planted)
                throw new NutrientApplicationDateBeforePlantDateException();

            AdministeredNutrient administeredNutrient = _mapper.Map<AdministeredNutrient>(model);
            administeredNutrient.Plant = plant;
            administeredNutrient.Nutrient = nutrient;
            await _database.AdministeredNutrients.AddAsync(administeredNutrient);
            await _database.SaveAsync();
            return administeredNutrient.Id;
        }

        public async Task<AdministeredNutrient> Update(Guid plantId, UpdateAdministeredNutrientModel model)
        {
            Plant plant = await _database.Plants
                .Include(x => x.AdministeredNutrients)
                .FirstOrDefaultAsync(x => x.Id == plantId);
            if (plant is null)
                throw new PlantDoesNotExistException(plantId);

            AdministeredNutrient administeredNutrient =
                plant.AdministeredNutrients.FirstOrDefault(x => x.Id == model.Id);
            if (administeredNutrient is null)
                return null;

            Nutrient nutrient = await _database.Nutrients.FindAsync(model.NutrientId);
            if (nutrient is null)
                throw new NutrientDoesNotExistException(model.NutrientId);

            if (model.Date < plant.Planted)
                throw new NutrientApplicationDateBeforePlantDateException();

            _mapper.Map(model, administeredNutrient);
            administeredNutrient.Plant = plant;
            administeredNutrient.Nutrient = nutrient;
            _database.AdministeredNutrients.Update(administeredNutrient);
            await _database.SaveAsync();
            return administeredNutrient;
        }

        public async Task Delete(Guid plantId, Guid id)
        {
            Plant plant = await _database.Plants
                .Include(x => x.AdministeredNutrients)
                .FirstOrDefaultAsync(x => x.Id == plantId);
            if (plant is null)
                return;

            AdministeredNutrient administeredNutrient = plant.AdministeredNutrients.FirstOrDefault(x => x.Id == id);
            if (administeredNutrient is null)
                return;

            plant.AdministeredNutrients.Remove(administeredNutrient);
            _database.AdministeredNutrients.Remove(administeredNutrient);
            await _database.SaveAsync();
        }
    }
}