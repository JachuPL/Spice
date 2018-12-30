using Spice.Application.Plants.Nutrients.Models;
using Spice.Domain.Plants;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Plants.Nutrients.Interfaces
{
    public interface ICommandPlantNutrients
    {
        Task<Guid> Create(Guid plantId, CreateAdministeredNutrientModel model);

        Task<AdministeredNutrient> Update(Guid plantId, UpdateAdministeredNutrientModel model);

        Task Delete(Guid plantId, Guid id);
    }
}