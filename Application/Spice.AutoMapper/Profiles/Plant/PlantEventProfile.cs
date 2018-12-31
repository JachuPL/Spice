using AutoMapper;
using Spice.Application.Plants.Events.Models;
using Spice.AutoMapper.Profiles.Plant.Converters;
using Spice.Domain.Plants.Events;
using Spice.ViewModels.Plants.Events;
using System;

namespace Spice.AutoMapper.Profiles.Plant
{
    public class PlantEventProfile : Profile
    {
        public PlantEventProfile()
        {
            CreateMap<CreatePlantEventViewModel, CreatePlantEventModel>();

            CreateMap<UpdatePlantEventViewModel, UpdatePlantEventModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.Empty));

            CreateMap<EventTypeViewModel, EventType>().ConvertUsing<EventTypeViewModelConverter>();

            CreateMap<EventType, EventTypeViewModel>().ConvertUsing<EventTypeConverter>();

            CreateMap<CreatePlantEventModel, Event>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.Empty))
                .ForMember(x => x.Plant, opt => opt.Ignore());

            CreateMap<UpdatePlantEventModel, Event>()
                .ForMember(x => x.Plant, opt => opt.Ignore());

            CreateMap<Event, PlantEventsIndexViewModel>();

            CreateMap<Event, PlantEventDetailsViewModel>();

            CreateMap<OccuredPlantEventsSummaryModel, OccuredPlantEventsSummaryViewModel>();
        }
    }
}