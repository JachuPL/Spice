using Spice.Domain;

namespace Spice.Application.Tests.Fields
{
    public static class ModelFactory
    {
        public static Field DomainModel(string fieldName = "Field A", double latitude = 52, double longtitude = 20)
        {
            return new Field()
            {
                Name = fieldName,
                Description = "Random field description",
                Latitude = latitude,
                Longtitude = longtitude
            };
        }
    }
}