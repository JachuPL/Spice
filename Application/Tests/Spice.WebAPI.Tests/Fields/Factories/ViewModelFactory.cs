using Spice.ViewModels.Fields;

namespace Spice.WebAPI.Tests.Fields.Factories
{
    internal static class ViewModelFactory
    {
        public static CreateFieldViewModel CreateValidCreationModel()
        {
            return new CreateFieldViewModel()
            {
            };
        }

        public static CreateFieldViewModel CreateInvalidCreationModel()
        {
            return new CreateFieldViewModel()
            {
            };
        }

        public static UpdateFieldViewModel CreateValidUpdateModel()
        {
            return new UpdateFieldViewModel()
            {
            };
        }

        public static UpdateFieldViewModel CreateInvalidUpdateModel()
        {
            return new UpdateFieldViewModel()
            {
            };
        }
    }
}