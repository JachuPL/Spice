using AutoMapper;
using Spice.Application.Plants.Models;
using Spice.AutoMapper.Profiles.Plant.Converters;
using Spice.Domain.Plants;
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

            CreateMap<CreatePlantModel, Domain.Plants.Plant>()
                .ForMember(x => x.Id, ex => ex.MapFrom(x => Guid.Empty))
                .ForMember(x => x.Field, opt => opt.Ignore())
                .ForMember(x => x.Species, opt => opt.Ignore())
                .ForMember(x => x.AdministeredNutrients, opt => opt.Ignore())
                .ForMember(x => x.Events, opt => opt.Ignore());

            CreateMap<UpdatePlantModel, Domain.Plants.Plant>()
                .ForMember(x => x.Field, opt => opt.Ignore())
                .ForMember(x => x.Species, opt => opt.Ignore())
                .ForMember(x => x.AdministeredNutrients, opt => opt.Ignore())
                .ForMember(x => x.Events, opt => opt.Ignore());

            CreateMap<Domain.Plants.Plant, PlantIndexViewModel>()
                .ForMember(x => x.Species, opt => opt.MapFrom(x => x.Species.LatinName));

            CreateMap<Domain.Plants.Plant, PlantDetailsViewModel>()
                .ForMember(x => x.Nutrients, opt => opt.MapFrom(x => x.AdministeredNutrients));
        }
    }
}