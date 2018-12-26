using Spice.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Fields.Interfaces
{
    public interface IQueryFields
    {
        Task<IEnumerable<Field>> GetAll();

        Task<Field> Get(Guid id);
    }
}