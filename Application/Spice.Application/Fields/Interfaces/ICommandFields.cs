using Spice.Application.Fields.Models;
using Spice.Domain;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Fields.Interfaces
{
    public interface ICommandFields
    {
        Task<Guid> Create(CreateFieldModel model);

        Task<Field> Update(UpdateFieldModel model);

        Task Delete(Guid id);
    }
}