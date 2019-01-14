using AutoMapper;
using Spice.Domain.Plants;
using Spice.ViewModels.Plants;

namespace Spice.AutoMapper.Profiles.Plant.Converters
{
    internal sealed class PlantStateConverter : ITypeConverter<PlantState, PlantStateViewModel>
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

                case PlantState.Sprouting:
                    return PlantStateViewModel.Sprouting;

                default:
                    return PlantStateViewModel.Healthy;
            }
        }
    }
}