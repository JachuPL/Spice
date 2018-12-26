namespace Spice.Application.Tests.Species
{
    public class ModelFactory
    {
        public static Domain.Plants.Species DomainModel()
        {
            return new Domain.Plants.Species()
            {
                Name = "Pepper",
                LatinName = "Capsicum annuum",
                Description = "Likes hot climate. Hates overwatering."
            };
        }
    }
}