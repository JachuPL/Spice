using AutoMapper;
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
        private readonly IMapper _mapper;

        public CommandPlants(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<Guid> Create(CreatePlantModel model)
        {
            Plant existingAtCoordinates =
                await _database.Plants.AsNoTracking().FirstOrDefaultAsync(x => x.Row == model.Row && x.Column == model.Column && x.FieldName == model.FieldName);

            if (existingAtCoordinates != null)
                throw new PlantExistsAtCoordinatesException(model.Row, model.Column);

            Plant plant = _mapper.Map<Plant>(model);

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

            _mapper.Map(model, plant);

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