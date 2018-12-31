using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Nutrients.Models;
using Spice.Application.Plants.Nutrients.Interfaces;
using Spice.Application.Plants.Nutrients.Models;
using Spice.Domain.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Application.Plants.Nutrients
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
            Plant plant = await GetPlantById(id);
            return plant?.AdministeredNutrients;
        }

        private async Task<Plant> GetPlantById(Guid id)
        {
            return await _database.Plants
                .Include(x => x.AdministeredNutrients).ThenInclude(x => x.Nutrient)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AdministeredNutrient> Get(Guid plantId, Guid id)
        {
            Plant plant = await GetPlantById(plantId);

            return plant?.AdministeredNutrients.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<AdministeredPlantNutrientsSummaryModel>> Sum(Guid plantId)
        {
            Plant plant = await GetPlantById(plantId);

            return plant?.AdministeredNutrients.GroupBy(x => x.Nutrient)
                .Select(x => new AdministeredPlantNutrientsSummaryModel()
                {
                    Nutrient = _mapper.Map<NutrientDetailsModel>(x.Key),
                    TotalAmount = x.Sum(z => z.Amount)
                }).ToList();
        }
    }
}