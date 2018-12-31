using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Species.Interfaces
{
    public interface IQuerySpecies
    {
        Task<IEnumerable<Domain.Species>> GetAll();

        Task<Domain.Species> Get(Guid id);
    }
}