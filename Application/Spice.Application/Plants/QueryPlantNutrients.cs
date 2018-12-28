using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Nutrients.Models;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Domain.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Application.Plants
{
    public class QueryPlantNutrients : IQueryPlantNutrients
    {
        private readonly IDatabaseService _database;
        private readonly IMapper _mapper;

        public QueryPlantNutrients(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AdministeredNutrient>> GetByPlant(Guid id)
        {
            Plant plant = await _database.Plants.Include(x => x.AdministeredNutrients)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (plant is null)
                throw new PlantDoesNotExistException(id);

            return plant.AdministeredNutrients;
        }

        public async Task<AdministeredNutrient> Get(Guid plantId, Guid id)
        {
            Plant plant = await _database.Plants.Include(x => x.AdministeredNutrients)
                .FirstOrDefaultAsync(x => x.Id == plantId);
            if (plant is null)
                throw new PlantDoesNotExistException(id);

            return plant.AdministeredNutrients.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<AdministeredPlantNutrientsSummaryModel>> Sum(Guid plantId)
        {
            Plant plant = await _database.Plants
                .Include(x => x.AdministeredNutrients).ThenInclude(x => x.Nutrient)
                .FirstOrDefaultAsync(x => x.Id == plantId);

            if (plant is null)
                throw new PlantDoesNotExistException(plantId);

            return plant.AdministeredNutrients.GroupBy(x => x.Nutrient)
                .Select(x => new AdministeredPlantNutrientsSummaryModel()
                {
                    Nutrient = _mapper.Map<NutrientDetailsModel>(x.Key),
                    TotalAmount = x.Sum(z => z.Amount)
                }).ToList();
        }
    }
}