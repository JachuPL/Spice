﻿using AutoMapper;
using Spice.Application.Nutrients.Models;
using Spice.Domain;
using Spice.ViewModels.Nutrients;
using System;

namespace Spice.AutoMapper.Profiles.Nutrients
{
    internal sealed class NutrientProfile : Profile
    {
        public NutrientProfile()
        {
            CreateMap<CreateNutrientViewModel, CreateNutrientModel>();

            CreateMap<UpdateNutrientViewModel, UpdateNutrientModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.Empty));

            CreateMap<CreateNutrientModel, Nutrient>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.Empty))
                .ForMember(x => x.AdministeredToPlants, opt => opt.Ignore());

            CreateMap<UpdateNutrientModel, Nutrient>()
                .ForMember(x => x.AdministeredToPlants, opt => opt.Ignore());

            CreateMap<Nutrient, NutrientIndexViewModel>();
            CreateMap<Nutrient, NutrientDetailsModel>();
            CreateMap<NutrientDetailsModel, NutrientDetailsViewModel>();
        }
    }
}