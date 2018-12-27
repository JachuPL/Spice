using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Species.Interfaces
{
    public interface IQuerySpecies
    {
        Task<IEnumerable<Domain.Plants.Species>> GetAll();

        Task<Domain.Plants.Species> Get(Guid id);
    }
}