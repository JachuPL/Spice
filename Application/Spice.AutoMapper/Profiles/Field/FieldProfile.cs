using AutoMapper;
using Spice.Application.Fields.Models;
using Spice.ViewModels.Fields;
using System;

namespace Spice.AutoMapper.Profiles.Field
{
    internal class FieldProfile : Profile
    {
        public FieldProfile()
        {
            CreateMap<CreateFieldViewModel, CreateFieldModel>();

            CreateMap<UpdateFieldViewModel, UpdateFieldModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.Empty));

            CreateMap<CreateFieldModel, Domain.Field>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.Empty))
                .ForMember(x => x.Plants, opt => opt.Ignore());

            CreateMap<Domain.Field, FieldIndexViewModel>();
            CreateMap<Domain.Field, FieldDetailsViewModel>();
        }
    }
}