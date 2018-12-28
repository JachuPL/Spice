using Spice.Application.Plants.Models;
using Spice.Domain.Plants;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Plants.Interfaces
{
    public interface ICommandPlantNutrients
    {
        Task<Guid> Create(CreateAdministeredNutrientModel model);

        Task<AdministeredNutrient> Update(UpdateAdministeredNutrientModel model);

        Task Delete(Guid id);
    }
}