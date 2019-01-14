using AutoMapper;
using Spice.Domain.Plants.Events;
using Spice.ViewModels.Plants.Events;

namespace Spice.AutoMapper.Profiles.Plant.Converters
{
    internal sealed class EventTypeConverter : ITypeConverter<EventType, EventTypeViewModel>
    {
        public EventTypeViewModel Convert(EventType source, EventTypeViewModel destination, ResolutionContext context)
        {
            switch (source)
            {
                case EventType.Disease:
                    return EventTypeViewModel.Disease;

                case EventType.Fungi:
                    return EventTypeViewModel.Fungi;

                case EventType.Growth:
                    return EventTypeViewModel.Growth;

                case EventType.Insects:
                    return EventTypeViewModel.Insects;

                case EventType.Moving:
                    return EventTypeViewModel.Moving;

                case EventType.OverWatering:
                    return EventTypeViewModel.OverWatering;

                case EventType.Pests:
                    return EventTypeViewModel.Pests;

                case EventType.UnderWatering:
                    return EventTypeViewModel.UnderWatering;

                case EventType.StartedTracking:
                    return EventTypeViewModel.StartedTracking;

                case EventType.Nutrition:
                    return EventTypeViewModel.Nutrition;

                case EventType.Sprouting:
                    return EventTypeViewModel.Sprouting;

                default:
                    return EventTypeViewModel.Insects;
            }
        }
    }
}