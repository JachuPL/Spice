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
                .AsNoTracking()
                .Include(x => x.AdministeredNutrients).ThenInclude(x => x.Nutrient)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AdministeredNutrient> Get(Guid plantId, Guid id)
        {
            Plant plant = await GetPlantById(plantId);

            return plant?.AdministeredNutrients.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<PlantNutrientAdministrationSummaryModel>> Summary(Guid plantId, DateTime? startDate = null, DateTime? endDate = null)
        {
            Plant existingPlant = await _database.Plants
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == plantId);
            if (existingPlant is null)
            {
                return null;
            }

            IQueryable<AdministeredNutrient> administeredNutrients =
                from administeredNutrient in _database.AdministeredNutrients.AsNoTracking()
                join plant in _database.Plants on administeredNutrient.Plant.Id equals plant.Id
                where plant.Id == existingPlant.Id
                select administeredNutrient;

            if (startDate.HasValue)
            {
                administeredNutrients = administeredNutrients.Where(x => startDate.Value <= x.Date);
            }

            if (endDate.HasValue)
            {
                administeredNutrients = administeredNutrients.Where(x => x.Date <= endDate.Value);
            }

            IQueryable<PlantNutrientAdministrationSummaryModel> administeredNutrientsByNutrient =
                from administeredNutrient in administeredNutrients
                group administeredNutrient by administeredNutrient.Nutrient
                into administeredNutrientNutrient
                select new PlantNutrientAdministrationSummaryModel
                {
                    Nutrient = _mapper.Map<NutrientDetailsModel>(administeredNutrientNutrient.Key),
                    TotalAmount = administeredNutrientNutrient.Sum(z => z.Amount),
                    FirstAdministration = administeredNutrientNutrient.Min(z => z.Date),
                    LastAdministration = administeredNutrientNutrient.Max(z => z.Date)
                };

            return await administeredNutrientsByNutrient.ToListAsync();
        }
    }
}