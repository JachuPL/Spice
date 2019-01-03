using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Nutrients.Interfaces;
using Spice.Application.Nutrients.Models;
using Spice.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Nutrients
{
    public class QueryNutrients : IQueryNutrients
    {
        private readonly IDatabaseService _database;
        private readonly IMapper _mapper;

        public QueryNutrients(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Nutrient>> GetAll()
        {
            return await _database.Nutrients
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<NutrientDetailsModel> Get(Guid id)
        {
            Nutrient nutrient = await _database.Nutrients
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<NutrientDetailsModel>(nutrient);
        }
    }
}