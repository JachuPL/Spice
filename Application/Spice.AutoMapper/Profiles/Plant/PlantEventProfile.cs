using AutoMapper;
using Spice.Application.Plants.Models;
using Spice.Domain.Plants.Events;
using Spice.ViewModels.Plants.OccuredEvents;
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

            CreateMap<CreatePlantEventModel, Event>();

            CreateMap<UpdatePlantEventModel, Event>();

            CreateMap<Event, PlantEventsIndexViewModel>();

            CreateMap<Event, PlantEventDetailsViewModel>();

            CreateMap<OccuredPlantEventsSummaryModel, OccuredPlantEventsSummaryViewModel>();
        }
    }
}