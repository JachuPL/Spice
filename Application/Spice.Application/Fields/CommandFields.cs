using AutoMapper;
using Spice.Application.Common;
using Spice.Application.Fields.Interfaces;
using Spice.Application.Fields.Models;
using Spice.Domain;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Fields
{
    public class CommandFields : ICommandFields
    {
        private readonly IDatabaseService _database;
        private readonly IMapper _mapper;

        public CommandFields(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public Task<Guid> Create(CreateFieldModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Field> Update(UpdateFieldModel model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}