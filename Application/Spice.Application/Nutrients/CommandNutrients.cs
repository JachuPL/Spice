using AutoMapper;
using Spice.Application.Common;
using Spice.Application.Nutrients.Interfaces;
using Spice.Application.Nutrients.Models;
using Spice.Domain;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Nutrients
{
    public class CommandNutrients : ICommandNutrients
    {
        private readonly IDatabaseService _database;
        private readonly IMapper _mapper;

        public CommandNutrients(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public Task<Guid> Create(CreateNutrientModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Nutrient> Update(UpdateNutrientModel model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}