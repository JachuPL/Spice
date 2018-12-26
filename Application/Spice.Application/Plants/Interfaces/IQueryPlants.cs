using Spice.Domain.Plants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Plants.Interfaces
{
    public interface IQueryPlants
    {
        Task<IEnumerable<Plant>> GetAll();

        Task<Plant> Get(Guid id);
    }
}