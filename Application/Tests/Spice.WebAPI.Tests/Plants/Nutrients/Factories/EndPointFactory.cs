namespace Spice.WebAPI.Tests.Plants.Nutrients.Factories
{
    internal static class EndPointFactory
    {
        public static string ListEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/nutrients";

        public static string DetailsEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/nutrients/DC117408-630E-459B-B16F-DE36EBC58E8F";

        public static string CreateEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/nutrients";

        public static string UpdateEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/nutrients/DC117408-630E-459B-B16F-DE36EBC58E8F";

        public static string DeleteEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/nutrients/DC117408-630E-459B-B16F-DE36EBC58E8F";

        public static string SumTotalNutrientsEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/nutrients/sum";

        public static string SumTotalNutrientsBetweenDatesEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/nutrients/sum?from=2018-12-20&to=2018-12-31";
    }
}