using Spice.ViewModels.Plants.OccuredEvents;

namespace Spice.WebAPI.Tests.Plants.Factories.Events
{
    internal static class ViewModelFactory
    {
        public static CreatePlantEventViewModel CreateValidCreationModel()
        {
            return new CreatePlantEventViewModel()
            {
            };
        }

        public static CreatePlantEventViewModel CreateInvalidCreationModel()
        {
            return new CreatePlantEventViewModel()
            {
            };
        }

        public static UpdatePlantEventViewModel CreateValidUpdateModel()
        {
            return new UpdatePlantEventViewModel()
            {
            };
        }

        public static UpdatePlantEventViewModel CreateInvalidUpdateModel()
        {
            return new UpdatePlantEventViewModel()
            {
            };
        }
    }
}