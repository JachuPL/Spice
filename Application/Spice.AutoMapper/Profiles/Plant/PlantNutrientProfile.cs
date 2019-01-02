using AutoMapper;
using Spice.Application.Plants.Nutrients.Models;
using Spice.Domain.Plants;
using Spice.ViewModels.Plants.Nutrients;
using System;

namespace Spice.AutoMapper.Profiles.Plant
{
    internal sealed class PlantNutrientProfile : Profile
    {
        public PlantNutrientProfile()
        {
            CreateMap<CreateAdministeredNutrientViewModel, CreateAdministeredNutrientModel>();

            CreateMap<UpdateAdministeredNutrientViewModel, UpdateAdministeredNutrientModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.Empty));

            CreateMap<CreateAdministeredNutrientModel, AdministeredNutrient>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.Empty))
                .ForMember(x => x.Nutrient, opt => opt.Ignore())
                .ForMember(x => x.Plant, opt => opt.Ignore());

            CreateMap<UpdateAdministeredNutrientModel, AdministeredNutrient>()
                .ForMember(x => x.Nutrient, opt => opt.Ignore())
                .ForMember(x => x.Plant, opt => opt.Ignore());

            CreateMap<AdministeredNutrient, AdministeredNutrientsIndexViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Nutrient.Name))
                .ForMember(x => x.Amount, opt => opt.MapFrom(x => $"{x.Amount} {x.Nutrient.DosageUnits}"));

            CreateMap<AdministeredNutrient, AdministeredNutrientDetailsViewModel>();

            CreateMap<PlantNutrientAdministrationCountModel, PlantNutrientAdministrationCountViewModel>();
        }
    }
}