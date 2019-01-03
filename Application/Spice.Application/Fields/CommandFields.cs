using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spice.Application.Common;
using Spice.Application.Fields.Exceptions;
using Spice.Application.Fields.Interfaces;
using Spice.Application.Fields.Models;
using Spice.Domain;
using System;
using System.Linq;
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

        public async Task<Guid> Create(CreateFieldModel model)
        {
            IQueryable<Field> existingFieldsWithName = from existingField in _database.Fields
                                                       where existingField.Name == model.Name
                                                       select existingField;
            if (await existingFieldsWithName.AnyAsync())
            {
                throw new FieldWithNameAlreadyExistsException(model.Name);
            }

            Field field = _mapper.Map<Field>(model);
            await _database.Fields.AddAsync(field);
            await _database.SaveAsync();
            return field.Id;
        }

        public async Task<Field> Update(UpdateFieldModel model)
        {
            Field field = await _database.Fields.FindAsync(model.Id);
            if (field is null)
            {
                return null;
            }

            IQueryable<Field> existingFieldsWithName = from existingField in _database.Fields
                                                       where (existingField.Name == model.Name) && (existingField.Id != field.Id)
                                                       select existingField;

            if (await existingFieldsWithName.AnyAsync())
            {
                throw new FieldWithNameAlreadyExistsException(model.Name);
            }

            _mapper.Map(model, field);
            _database.Fields.Update(field);
            await _database.SaveAsync();
            return field;
        }

        public async Task Delete(Guid id)
        {
            Field field = await _database.Fields.FindAsync(id);
            if (field is null)
            {
                return;
            }

            _database.Fields.Remove(field);
            await _database.SaveAsync();
        }
    }
}