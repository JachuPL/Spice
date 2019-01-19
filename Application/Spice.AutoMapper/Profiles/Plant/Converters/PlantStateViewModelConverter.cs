using AutoMapper;
using Spice.Domain.Plants;
using Spice.ViewModels.Plants;

namespace Spice.AutoMapper.Profiles.Plant.Converters
{
    internal sealed class PlantStateViewModelConverter : ITypeConverter<PlantStateViewModel, PlantState>
    {
        public PlantState Convert(PlantStateViewModel source, PlantState destination, ResolutionContext context)
        {
            switch (source)
            {
                case PlantStateViewModel.Healthy:
                    return PlantState.Healthy;

                case PlantStateViewModel.Flowering:
                    return PlantState.Flowering;

                case PlantStateViewModel.Fruiting:
                    return PlantState.Fruiting;

                case PlantStateViewModel.Harvested:
                    return PlantState.Harvested;

                case PlantStateViewModel.Sick:
                    return PlantState.Sick;

                case PlantStateViewModel.Deceased:
                    return PlantState.Deceased;

                case PlantStateViewModel.Sprouting:
                    return PlantState.Sprouting;

                default:
                    return PlantState.Healthy;
            }
        }
    }
}