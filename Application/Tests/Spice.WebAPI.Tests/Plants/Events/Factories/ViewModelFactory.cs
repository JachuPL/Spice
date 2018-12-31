using Spice.ViewModels.Plants.Events;
using System;

namespace Spice.WebAPI.Tests.Plants.Events.Factories
{
    internal static class ViewModelFactory
    {
        public static CreatePlantEventViewModel CreateValidCreationModel()
        {
            return new CreatePlantEventViewModel()
            {
                Type = EventTypeViewModel.UnderWatering,
                Description = "This plant quickly absorbs water and needs to be watered more often from now on.",
                Occured = DateTime.Now.AddDays(-1)
            };
        }

        public static CreatePlantEventViewModel CreateInvalidCreationModel()
        {
            return new CreatePlantEventViewModel()
            {
                Type = (EventTypeViewModel)999,
                Description = "A",
                Occured = DateTime.Now.AddDays(1)
            };
        }

        public static UpdatePlantEventViewModel CreateValidUpdateModel()
        {
            return new UpdatePlantEventViewModel()
            {
                Type = EventTypeViewModel.UnderWatering,
                Description = "This plant quickly absorbs water and needs to be watered more often from now on.",
                Occured = DateTime.Now.AddDays(-1)
            };
        }

        public static UpdatePlantEventViewModel CreateInvalidUpdateModel()
        {
            return new UpdatePlantEventViewModel()
            {
                Type = (EventTypeViewModel)999,
                Description = "A",
                Occured = DateTime.Now.AddDays(1)
            };
        }
    }
}