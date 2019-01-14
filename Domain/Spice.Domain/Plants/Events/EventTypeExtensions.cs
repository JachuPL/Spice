using System.Linq;

namespace Spice.Domain.Plants.Events
{
    public static class EventTypeExtensions
    {
        private static readonly EventType[] UnchangeableTypes = { EventType.Moving, EventType.StartedTracking };

        public static bool IsChangeable(this EventType type)
        {
            return !UnchangeableTypes.Contains(type);
        }

        public static bool IsCreationRestricted(this EventType type)
        {
            return UnchangeableTypes.Contains(type);
        }
    }
}