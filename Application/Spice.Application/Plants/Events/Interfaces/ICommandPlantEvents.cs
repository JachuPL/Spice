using Spice.Application.Plants.Events.Models;
using Spice.Domain.Plants.Events;
using System;
using System.Threading.Tasks;

namespace Spice.Application.Plants.Events.Interfaces
{
    public interface ICommandPlantEvents
    {
        Task<Guid> Create(Guid plantId, CreatePlantEventModel model);

        Task<Event> Update(Guid plantId, UpdatePlantEventModel model);

        Task Delete(Guid plantId, Guid id);
    }
}