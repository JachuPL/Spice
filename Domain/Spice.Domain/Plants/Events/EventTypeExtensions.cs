using System;
using System.Linq;

namespace Spice.Domain.Plants.Events
{
    public static class EventTypeExtensions
    {
        private static readonly EventType[] UnchangeableTypes = { EventType.Moving, EventType.Start };

        public static bool IsChangeable(this EventType type)
        {
            return !UnchangeableTypes.Contains(type);
        }
    }
}