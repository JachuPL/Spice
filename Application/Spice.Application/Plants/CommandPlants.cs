using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Fields.Exceptions;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Application.Species.Exceptions;
using Spice.Domain;
using Spice.Domain.Builders;
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
            {
                throw new FieldNotFoundException(model.FieldId);
            }

            IQueryable<Plant> plantsExistingAtCoordinates =
                from existingPlantsAtCoordinates in _database.Plants
                where (existingPlantsAtCoordinates.Field.Id == field.Id)
                      && (existingPlantsAtCoordinates.Row == model.Row) &&
                      (existingPlantsAtCoordinates.Column == model.Column) && (existingPlantsAtCoordinates.Field.Id == model.FieldId)
                select existingPlantsAtCoordinates;

            if (await plantsExistingAtCoordinates.AnyAsync())
            {
                throw new PlantExistsAtCoordinatesException(model.Row, model.Column);
            }

            Domain.Species species = await _database.Species.FindAsync(model.SpeciesId);
            if (species is null)
            {
                throw new SpeciesNotFoundException(model.SpeciesId);
            }

            Plant plant = New.Plant.WithName(model.Name)
                             .WithSpecies(species)
                             .WithField(field)
                             .InRow(model.Row)
                             .InColumn(model.Column)
                             .WithState(model.State);
            await _database.Plants.AddAsync(plant);
            await _database.SaveAsync();

            return plant.Id;
        }

        public async Task<Plant> Update(UpdatePlantModel model)
        {
            Plant plant = await _database.Plants
                .Include(x => x.Field)
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            if (plant is null)
            {
                return null;
            }

            Field field = await _database.Fields
                .Include(x => x.Plants)
                .FirstOrDefaultAsync(x => x.Id == model.FieldId);
            if (field is null)
            {
                throw new FieldNotFoundException(model.FieldId);
            }

            if (field.Plants.Any(x => (x.Row == model.Row) && (x.Column == model.Column) && (x.Id != model.Id)))
            {
                throw new PlantExistsAtCoordinatesException(model.Row, model.Column);
            }

            Domain.Species species = await _database.Species.FirstOrDefaultAsync(x => x.Id == model.SpeciesId);
            if (species is null)
            {
                throw new SpeciesNotFoundException(model.SpeciesId);
            }

            _mapper.Map(model, plant);
            plant.ChangeField(field);
            plant.Species = species;
            _database.Plants.Update(plant);
            await _database.SaveAsync();

            return plant;
        }

        public async Task Delete(Guid id)
        {
            Plant plant = await _database.Plants.FindAsync(id);
            if (plant is null)
            {
                return;
            }

            _database.Plants.Remove(plant);
            await _database.SaveAsync();
        }
    }
}