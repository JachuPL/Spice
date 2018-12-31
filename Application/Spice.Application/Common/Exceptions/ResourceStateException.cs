using System;

namespace Spice.Application.Common.Exceptions
{
    public abstract class ResourceStateException : Exception
    {
        protected ResourceStateException(string message) : base(message)
        {
        }
    }
}