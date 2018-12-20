using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Application.Plants
{
    public class CommandPlants : ICommandPlants
    {
        private readonly IDatabaseService _database;

        public CommandPlants(IDatabaseService database)
        {
            _database = database;
        }

        public async Task<Guid> Create(CreatePlantModel model)
        {
            Plant existingAtCoordinates =
                await _database.Plants.AsNoTracking().FirstOrDefaultAsync(x => x.Row == model.Row && x.Column == model.Column && x.FieldName == model.FieldName);

            if (existingAtCoordinates != null)
                throw new PlantExistsAtCoordinatesException(model.Row, model.Column);

            Plant plant = new Plant
            {
                Name = model.Name,
                Specimen = model.Specimen,
                FieldName = model.FieldName,
                Row = model.Row,
                Column = model.Column,
                Planted = model.Planted,
                State = model.State
            };

            await _database.Plants.AddAsync(plant);
            await _database.SaveAsync();

            return plant.Id;
        }

        public async Task<Plant> Update(UpdatePlantModel model)
        {
            Plant plant = await _database.Plants.FindAsync(model.Id);
            if (plant is null)
                return null;

            if (_database.Plants.AsNoTracking().Any(x => x.Row == model.Row && x.Column == model.Column && x.FieldName == model.FieldName))
                throw new PlantExistsAtCoordinatesException(model.Row, model.Column);

            plant.Name = model.Name;
            plant.Specimen = model.Specimen;
            plant.FieldName = model.FieldName;
            plant.Row = model.Row;
            plant.Column = model.Column;
            plant.Planted = model.Planted;
            plant.State = model.State;

            _database.Plants.Update(plant);
            await _database.SaveAsync();

            return plant;
        }

        public async Task Delete(Guid id)
        {
            Plant plant = await _database.Plants.FindAsync(id);
            if (plant is null)
                return;

            _database.Plants.Remove(plant);
            await _database.SaveAsync();
        }
    }
}