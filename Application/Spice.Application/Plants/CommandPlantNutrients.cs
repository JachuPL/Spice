using AutoMapper;
using Spice.Application.Common;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Domain.Plants;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Plants
{
    public class CommandPlantNutrients : ICommandPlantNutrients
    {
        private readonly IDatabaseService _database;
        private readonly IMapper _mapper;

        public CommandPlantNutrients(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public Task<Guid> Create(CreateAdministeredNutrientModel model)
        {
            throw new NotImplementedException();
        }

        public Task<AdministeredNutrient> Update(UpdateAdministeredNutrientModel model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid plantId, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}