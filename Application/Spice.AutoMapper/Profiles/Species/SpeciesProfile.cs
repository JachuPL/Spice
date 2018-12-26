using AutoMapper;
using Spice.Application.Species.Models;
using Spice.ViewModels.Species;
using System;

namespace Spice.AutoMapper.Profiles.Species
{
    internal class SpeciesProfile : Profile
    {
        public SpeciesProfile()
        {
            CreateMap<CreateSpeciesViewModel, CreateSpeciesModel>();

            CreateMap<UpdateSpeciesViewModel, UpdateSpeciesModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.Empty));

            CreateMap<CreateSpeciesModel, Domain.Plants.Species>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.Empty))
                .ForMember(x => x.Plants, opt => opt.Ignore());

            CreateMap<UpdateSpeciesModel, Domain.Plants.Species>()
                .ForMember(x => x.Plants, opt => opt.Ignore());

            CreateMap<Domain.Plants.Species, SpeciesIndexViewModel>();
            CreateMap<Domain.Plants.Species, SpeciesDetailsViewModel>();
        }
    }
}