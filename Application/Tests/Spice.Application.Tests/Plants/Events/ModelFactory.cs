using Spice.Application.Plants.Events.Models;
using Spice.Domain.Plants;
using Spice.Domain.Plants.Events;
using System;

namespace Spice.Application.Tests.Plants.Events
{
    internal static class ModelFactory
    {
        public static CreatePlantEventModel CreationModel(DateTime? occurenceDate = null, EventType type = EventType.Disease)
        {
            return new CreatePlantEventModel()
            {
                Type = type,
                Description = "Spotted a disease yesterday.",
                Occured = occurenceDate ?? DateTime.Now
            };
        }

        public static UpdatePlantEventModel UpdateModel(Guid? id = null, DateTime? occurenceDate = null, EventType type = EventType.Disease)
        {
            return new UpdatePlantEventModel()
            {
                Id = id ?? Guid.NewGuid(),
                Type = type,
                Description = "Spotted a disease yesterday.",
                Occured = occurenceDate ?? DateTime.Now
            };
        }

        public static Event DomainModel(Plant plant = null, EventType type = EventType.Fungi)
        {
            return new Event()
            {
                Plant = plant ?? Plants.ModelFactory.DomainModel(),
                Type = type,
                Description = "Spotted some fungi on the leaves.",
                Occured = DateTime.Now
            };
        }
    }
}