using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Nutrients.Exceptions;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Nutrients.Exceptions;
using Spice.Application.Plants.Nutrients.Interfaces;
using Spice.Application.Plants.Nutrients.Models;
using Spice.Domain;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Application.Plants.Nutrients
{
    public class CommandPlantNutrients : ICommandPlantNutrients
    {
        private readonly IDatabaseService _database;

        public CommandPlantNutrients(IDatabaseService database, IMapper mapper)
        {
            _database = database;
        }

        public async Task<Guid> Create(Guid plantId, CreateAdministeredNutrientModel model)
        {
            Plant plant = await _database.Plants.FindAsync(plantId);
            if (plant is null)
                throw new PlantNotFoundException(plantId);

            Nutrient nutrient = await _database.Nutrients.FindAsync(model.NutrientId);
            if (nutrient is null)
                throw new NutrientNotFoundException(model.NutrientId);

            if (model.Date < plant.Planted)
                throw new NutrientAdministrationDateBeforePlantDateException();

            AdministeredNutrient administeredNutrient = new AdministeredNutrient(plant, nutrient, model.Amount);
            await _database.AdministeredNutrients.AddAsync(administeredNutrient);

            if (model.CreateEvent)
            {
                Event @event = new Event(plant,
                    EventType.Nutrition,
                    $"Given {model.Amount} {nutrient.DosageUnits} of {nutrient.Name} to {plant.Name}. (Generated automatically)",
                    model.Date);
                plant.Events.Add(@event);
                _database.Plants.Update(plant);
            }

            await _database.SaveAsync();
            return administeredNutrient.Id;
        }

        public async Task<AdministeredNutrient> Update(Guid plantId, UpdateAdministeredNutrientModel model)
        {
            Plant plant = await _database.Plants
                .Include(x => x.AdministeredNutrients)
                .FirstOrDefaultAsync(x => x.Id == plantId);
            if (plant is null)
                throw new PlantNotFoundException(plantId);

            AdministeredNutrient administeredNutrient =
                plant.AdministeredNutrients.FirstOrDefault(x => x.Id == model.Id);
            if (administeredNutrient is null)
                return null;

            Nutrient nutrient = await _database.Nutrients.FindAsync(model.NutrientId);
            if (nutrient is null)
                throw new NutrientNotFoundException(model.NutrientId);

            if (model.Date < plant.Planted)
                throw new NutrientAdministrationDateBeforePlantDateException();

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