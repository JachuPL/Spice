using AutoMapper;
using Spice.Domain;
using Spice.ViewModels.Plants;

namespace Spice.AutoMapper.Profiles.Plant.Converters
{
    internal class PlantStateViewModelConverter : ITypeConverter<PlantStateViewModel, PlantState>
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

                default:
                    return PlantState.Healthy;
            }
        }
    }
}