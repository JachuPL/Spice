using Spice.Application.Plants.Events.Models.Summary;
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

        Task<IEnumerable<PlantEventOccurenceCountModel>> Summary(Guid plantId, DateTime? startDate = null, DateTime? endDate = null);
    }
}