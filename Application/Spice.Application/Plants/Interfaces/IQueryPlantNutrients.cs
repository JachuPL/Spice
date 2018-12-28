using Spice.Application.Plants.Models;
using Spice.Domain.Plants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Plants.Interfaces
{
    public interface IQueryPlantNutrients
    {
        Task<IEnumerable<AdministeredNutrient>> GetByPlant(Guid id);

        Task<AdministeredNutrient> Get(Guid plantId, Guid id);

        Task<IEnumerable<AdministeredPlantNutrientsSummaryModel>> Sum(Guid plantId);
    }
}