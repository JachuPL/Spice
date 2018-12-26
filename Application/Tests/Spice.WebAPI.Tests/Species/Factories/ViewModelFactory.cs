using Spice.ViewModels.Species;

namespace Spice.WebAPI.Tests.Species.Factories
{
    internal static class ViewModelFactory
    {
        public static CreateSpeciesViewModel CreateValidCreationModel()
        {
            return new CreateSpeciesViewModel()
            {
                Name = "Pepper",
                LatinName = "Capsicum annuum",
                Description = "Some cultivars of this species yield very spicy fruits."
            };
        }

        public static CreateSpeciesViewModel CreateInvalidCreationModel()
        {
            return new CreateSpeciesViewModel()
            {
                Name = "A",
                LatinName = "B",
                Description = "Some cultivars of this species yield very spicy fruits."
            };
        }

        public static UpdateSpeciesViewModel CreateValidUpdateModel()
        {
            return new UpdateSpeciesViewModel()
            {
                Name = "Chinese Pepper",
                LatinName = "Capsicum chinense",
                Description = "Some cultivars of this species yield very spicy fruits. Cultivated in eastern Asian regions."
            };
        }

        public static UpdateSpeciesViewModel CreateInvalidUpdateModel()
        {
            return new UpdateSpeciesViewModel()
            {
                Name = "A",
                LatinName = "B",
                Description = "Some cultivars of this species yield very spicy fruits."
            };
        }
    }
}