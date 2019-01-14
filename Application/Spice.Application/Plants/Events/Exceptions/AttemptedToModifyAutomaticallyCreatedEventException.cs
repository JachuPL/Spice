using Spice.Application.Common.Exceptions;

namespace Spice.Application.Plants.Events.Exceptions
{
    public class AttemptedToModifyAutomaticallyCreatedEventException : ResourceStateException
    {
        public AttemptedToModifyAutomaticallyCreatedEventException(string message) : base(message)
        {
        }
    }
}