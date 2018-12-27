using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Fields.Exceptions;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Application.Species.Exceptions;
using Spice.Domain;
using Spice.Domain.Plants;
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
            Field field = await _database.Fields.FirstOrDefaultAsync(x => x.Id == model.FieldId);
            if (field is null)
                throw new FieldDoesNotExistException(model.FieldId);

            Plant existingAtCoordinates =
                await _database.Plants.AsNoTracking().FirstOrDefaultAsync(x => x.Row == model.Row && x.Column == model.Column && x.Field.Id == model.FieldId);

            if (existingAtCoordinates != null)
                throw new PlantExistsAtCoordinatesException(model.Row, model.Column);

            Domain.Plants.Species species = await _database.Species.FirstOrDefaultAsync(x => x.Id == model.SpeciesId);
            if (species is null)
                throw new SpeciesDoesNotExistException(model.SpeciesId);

            Plant plant = _mapper.Map<Plant>(model);
            plant.Field = field;
            field.Plants.Add(plant);
            plant.Species = species;
            species.Plants.Add(plant);

            await _database.Plants.AddAsync(plant);
            await _database.SaveAsync();

            return plant.Id;
        }

        public async Task<Plant> Update(UpdatePlantModel model)
        {
            Plant plant = await _database.Plants.FindAsync(model.Id);
            if (plant is null)
                return null;

            Field field = await _database.Fields
                .Include(x => x.Plants)
                .FirstOrDefaultAsync(x => x.Id == model.FieldId);
            if (field is null)
                throw new FieldDoesNotExistException(model.FieldId);

            if (field.Plants.Any(x => x.Row == model.Row && x.Column == model.Column && x.Id != model.Id))
                throw new PlantExistsAtCoordinatesException(model.Row, model.Column);

            Domain.Plants.Species species = await _database.Species.FirstOrDefaultAsync(x => x.Id == model.SpeciesId);
            if (species is null)
                throw new SpeciesDoesNotExistException(model.SpeciesId);

            _mapper.Map(model, plant);
            plant.Field = field;
            plant.Species = species;
            field.Plants.Add(plant);

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