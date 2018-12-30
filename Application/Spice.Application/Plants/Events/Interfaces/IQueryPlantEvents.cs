using Spice.Application.Plants.Events.Models;
using Spice.Domain.Plants.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Application.Plants.Events.Interfaces
{
    public interface IQueryPlantEvents
    {
        Task<IEnumerable<Event>> GetByPlant(Guid plantId);

        Task<Event> Get(Guid plantId, Guid id);

        Task<IEnumerable<OccuredPlantEventsSummaryModel>> Sum(Guid plantId);
    }
}