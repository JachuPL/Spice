using Spice.ViewModels.Fields;

namespace Spice.WebAPI.Tests.Fields.Factories
{
    internal static class ViewModelFactory
    {
        public static CreateFieldViewModel CreateValidCreationModel()
        {
            return new CreateFieldViewModel
            {
                Name = "Field A",
                Description = "Lots of sun in the morning and in the early afternoon. Formerly there was a barn.",
                Latitude = 50.9657062,
                Longtitude = 22.3966112
            };
        }

        public static CreateFieldViewModel CreateInvalidCreationModel()
        {
            return new CreateFieldViewModel
            {
                Name = "A",
                Description = "B",
                Latitude = 150.9657062,
                Longtitude = 222.3966112
            };
        }

        public static UpdateFieldViewModel CreateValidUpdateModel()
        {
            return new UpdateFieldViewModel
            {
                Name = "Field B",
                Description = "Opposite site of the river. High risk of flooding on early spring.",
                Latitude = 50.9659117,
                Longtitude = 22.395711
            };
        }

        public static UpdateFieldViewModel CreateInvalidUpdateModel()
        {
            return new UpdateFieldViewModel
            {
                Name = "B",
                Description = "C",
                Latitude = 250.9659117,
                Longtitude = 122.395711
            };
        }
    }
}