﻿using AutoMapper;
using Spice.Application.Plants.Models;
using Spice.Domain.Plants;
using Spice.ViewModels.Plants.AdministeredNutrients;
using System;

namespace Spice.AutoMapper.Profiles.Plant
{
    public class PlantNutrientProfile : Profile
    {
        public PlantNutrientProfile()
        {
            CreateMap<CreateAdministeredNutrientViewModel, CreateAdministeredNutrientModel>();

            CreateMap<UpdateAdministeredNutrientViewModel, UpdateAdministeredNutrientModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.Empty));

            CreateMap<CreateAdministeredNutrientModel, AdministeredNutrient>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.Empty))
                .ForMember(x => x.Nutrient, opt => opt.Ignore());

            CreateMap<UpdateAdministeredNutrientModel, AdministeredNutrient>()
                .ForMember(x => x.Nutrient, opt => opt.Ignore());

            CreateMap<AdministeredNutrient, AdministeredNutrientsIndexViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Nutrient.Name))
                .ForMember(x => x.Amount, opt => opt.MapFrom(x => $"{x.Amount} {x.Nutrient.DosageUnits}"));

            CreateMap<AdministeredNutrient, AdministeredNutrientDetailsViewModel>();

            CreateMap<AdministeredPlantNutrientsSummaryModel, AdministeredPlantNutrientsSummaryViewModel>();
        }
    }
}