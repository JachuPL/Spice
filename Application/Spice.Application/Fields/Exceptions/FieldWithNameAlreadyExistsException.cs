using System;

namespace Spice.Application.Fields.Exceptions
{
    public class FieldWithNameAlreadyExistsException : ArgumentException
    {
        public FieldWithNameAlreadyExistsException(string name) : base($"Field with name {name} already exists.")
        {
        }
    }
}