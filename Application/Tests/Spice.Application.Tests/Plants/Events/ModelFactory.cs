using Spice.Application.Plants.Models;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using System;

namespace Spice.Application.Tests.Plants.Events
{
    public static class ModelFactory
    {
        public static CreatePlantEventModel CreationModel(DateTime? occurenceDate = null)
        {
            return new CreatePlantEventModel()
            {
                Type = EventType.Moving,
                Description = "Moving plant to more sunny field.",
                Occured = occurenceDate ?? DateTime.Now
            };
        }

        public static UpdatePlantEventModel UpdateModel(Guid? id = null, DateTime? occurenceDate = null)
        {
            return new UpdatePlantEventModel()
            {
                Id = id ?? Guid.NewGuid(),
                Type = EventType.Moving,
                Description = "Moving plant to more sunny field.",
                Occured = occurenceDate ?? DateTime.Now
            };
        }

        public static Event DomainModel(Plant plant = null, EventType type = EventType.Moving)
        {
            return new Event()
            {
                Plant = plant ?? Plants.ModelFactory.DomainModel(),
                Type = type,
                Description = "Moving plant to more sunny field.",
                Occured = DateTime.Now
            };
        }
    }
}