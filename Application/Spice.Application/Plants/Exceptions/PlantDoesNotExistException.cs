using System;

namespace Spice.Application.Plants.Exceptions
{
    public class PlantDoesNotExistException : ArgumentException
    {
        public PlantDoesNotExistException(Guid id) : base($"No plant by id {id}")
        {
        }
    }
}