using System;

namespace Spice.Application.Plants.Exceptions
{
    public class PlantExistsAtCoordinatesException : ArgumentException
    {
        public PlantExistsAtCoordinatesException(int row, int column) : base($"There is another plant already growing in row #{row}, column #{column}")
        {
        }
    }
}