using Spice.Application.Common.Exceptions;

namespace Spice.Application.Fields.Exceptions
{
    public class FieldWithNameAlreadyExistsException : ResourceStateException
    {
        public FieldWithNameAlreadyExistsException(string name) : base($"Field with name {name} already exists.")
        {
        }
    }
}