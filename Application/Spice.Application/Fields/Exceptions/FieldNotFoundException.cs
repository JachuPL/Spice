using Spice.Application.Common.Exceptions;
using Spice.Domain;
using System;

namespace Spice.Application.Fields.Exceptions
{
    public class FieldNotFoundException : ResourceNotFoundException
    {
        public FieldNotFoundException(Guid id) : base(typeof(Field), id)
        {
        }
    }
}