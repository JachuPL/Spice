using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Nutrients.Models;
using Spice.Application.Species.Interfaces;
using Spice.Application.Species.Models;
using Spice.Domain.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Application.Species
{
    public class QuerySpecies : IQuerySpecies
    {
        private readonly IDatabaseService _database;
        private readonly IMapper _mapper;

        public QuerySpecies(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Domain.Species>> GetAll()
        {
            return await _database.Species
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Domain.Species> Get(Guid id)
        {
            return await _database.Species
                .Include(x => x.Plants)
                .ThenInclude(x => x.Field)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<SpeciesNutritionSummaryModel>> Summary(Guid id, DateTime? fromDate = null, DateTime? toDate = null)
        {
            Domain.Species species = await _database.Species.FindAsync(id);
            if (species is null)
                return null;

            IQueryable<AdministeredNutrient> administeredNutrients =
                from nutrients in _database.AdministeredNutrients.AsNoTracking()
                join plant in _database.Plants on nutrients.Plant.Id equals plant.Id
                join Species in _database.Species on plant.Species.Id equals Species.Id
                where plant.Species.Id == id
                select nutrients;

            if (fromDate.HasValue)
                administeredNutrients = administeredNutrients.Where(x => fromDate.Value <= x.Date);

            if (toDate.HasValue)
                administeredNutrients = administeredNutrients.Where(x => x.Date <= toDate.Value);

            IQueryable<SpeciesNutritionSummaryModel> administeredNutrientsBySpecies =
                from administeredNutrient in administeredNutrients
                group administeredNutrient by administeredNutrient.Nutrient into administeredNutrientsNutrient
                select new SpeciesNutritionSummaryModel()
                {
                    Nutrient = _mapper.Map<NutrientDetailsModel>(administeredNutrientsNutrient.Key),
                    TotalAmount = administeredNutrientsNutrient.Sum(z => z.Amount),
                    FirstAdministration = administeredNutrientsNutrient.Min(z => z.Date),
                    LastAdministration = administeredNutrientsNutrient.Max(z => z.Date)
                };

            return await administeredNutrientsBySpecies.ToListAsync();
        }
    }
}