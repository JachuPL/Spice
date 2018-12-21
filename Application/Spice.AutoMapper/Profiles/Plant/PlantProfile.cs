using AutoMapper;
using Spice.Application.Plants.Models;
using Spice.AutoMapper.Profiles.Plant.Converters;
using Spice.Domain;
using Spice.ViewModels.Plants;
using System;

namespace Spice.AutoMapper.Profiles.Plant
{
    internal class PlantProfile : Profile
    {
        public PlantProfile()
        {
            CreateMap<CreatePlantViewModel, CreatePlantModel>();

            CreateMap<UpdatePlantViewModel, UpdatePlantModel>()
                .ForMember(x => x.Id, ex => ex.MapFrom(x => Guid.Empty));

            CreateMap<PlantStateViewModel, PlantState>().ConvertUsing<PlantStateViewModelConverter>();

            CreateMap<PlantState, PlantStateViewModel>().ConvertUsing<PlantStateConverter>();
        }
    }
}