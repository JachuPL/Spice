using Spice.Application.Nutrients.Models;
using Spice.Domain;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Nutrients.Interfaces
{
    public interface ICommandNutrients
    {
        Task<Guid> Create(CreateNutrientModel model);

        Task<Nutrient> Update(UpdateNutrientModel model);

        Task Delete(Guid id);
    }
}