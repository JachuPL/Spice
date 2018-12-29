using Spice.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spice.Application.Nutrients.Models;

namespace Spice.Application.Nutrients.Interfaces
{
    public interface IQueryNutrients
    {
        Task<IEnumerable<Nutrient>> GetAll();

        Task<NutrientDetailsModel> Get(Guid id);
    }
}