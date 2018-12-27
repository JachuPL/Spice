using Spice.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Nutrients.Interfaces
{
    public interface IQueryNutrients
    {
        Task<IEnumerable<Nutrient>> GetAll();

        Task<Nutrient> Get(Guid id);
    }
}