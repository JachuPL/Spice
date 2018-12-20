using Spice.Application.Plants.Models;
using Spice.Domain;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Plants.Interfaces
{
    public interface ICommandPlants
    {
        Task<Guid> Create(CreatePlantModel model);

        Task<Plant> Update(UpdatePlantModel model);

        Task Delete(Guid ignored);
    }
}