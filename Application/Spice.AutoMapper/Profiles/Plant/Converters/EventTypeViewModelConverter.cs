using AutoMapper;
using Spice.Domain.Plants.Events;
using Spice.ViewModels.Plants.Events;

namespace Spice.AutoMapper.Profiles.Plant.Converters
{
    internal class EventTypeViewModelConverter : ITypeConverter<EventTypeViewModel, EventType>
    {
        public EventType Convert(EventTypeViewModel source, EventType destination, ResolutionContext context)
        {
            switch (source)
            {
                case EventTypeViewModel.Disease:
                    return EventType.Disease;

                case EventTypeViewModel.Fungi:
                    return EventType.Fungi;

                case EventTypeViewModel.Growth:
                    return EventType.Growth;

                case EventTypeViewModel.Insects:
                    return EventType.Insects;

                case EventTypeViewModel.Moving:
                    return EventType.Moving;

                case EventTypeViewModel.OverWatering:
                    return EventType.OverWatering;

                case EventTypeViewModel.Pests:
                    return EventType.Pests;

                case EventTypeViewModel.UnderWatering:
                    return EventType.UnderWatering;

                default:
                    return EventType.Insects;
            }
        }
    }
}