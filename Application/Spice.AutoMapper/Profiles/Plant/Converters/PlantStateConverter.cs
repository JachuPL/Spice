using AutoMapper;
using Spice.Domain;
using Spice.ViewModels.Plants;

namespace Spice.AutoMapper.Profiles.Plant.Converters
{
    internal class PlantStateConverter : ITypeConverter<PlantState, PlantStateViewModel>
    {
        public PlantStateViewModel Convert(PlantState source, PlantStateViewModel destination, ResolutionContext context)
        {
            switch (source)
            {
                case PlantState.Healthy:
                    return PlantStateViewModel.Healthy;

                case PlantState.Flowering:
                    return PlantStateViewModel.Flowering;

                case PlantState.Fruiting:
                    return PlantStateViewModel.Fruiting;

                case PlantState.Harvested:
                    return PlantStateViewModel.Harvested;

                case PlantState.Sick:
                    return PlantStateViewModel.Sick;

                case PlantState.Deceased:
                    return PlantStateViewModel.Deceased;

                default:
                    return PlantStateViewModel.Healthy;
            }
        }
    }
}