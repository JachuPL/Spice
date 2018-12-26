using Spice.Application.Species.Models;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Species.Interfaces
{
    public interface ICommandSpecies
    {
        Task<Guid> Create(CreateSpeciesModel model);

        Task<Domain.Plants.Species> Update(UpdateSpeciesModel model);

        Task Delete(Guid id);
    }
}