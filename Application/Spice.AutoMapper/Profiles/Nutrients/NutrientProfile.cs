using AutoMapper;
using Spice.Application.Nutrients.Models;
using Spice.ViewModels.Nutrients;
using System;

namespace Spice.AutoMapper.Profiles.Nutrients
{
    internal class NutrientProfile : Profile
    {
        public NutrientProfile()
        {
            CreateMap<CreateNutrientViewModel, CreateNutrientModel>();

            CreateMap<UpdateNutrientViewModel, UpdateNutrientModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.Empty));

            CreateMap<CreateNutrientModel, Domain.Nutrient>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.Empty));

            CreateMap<UpdateNutrientModel, Domain.Nutrient>();

            CreateMap<Domain.Nutrient, NutrientIndexViewModel>();
            CreateMap<Domain.Nutrient, NutrientDetailsViewModel>();
        }
    }
}