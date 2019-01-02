namespace Spice.WebAPI.Tests.Species.Factories
{
    internal static class EndPointFactory
    {
        public static string ListEndpoint() => "/api/species";

        public static string DetailsEndpoint() => "/api/species/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";

        public static string CreateEndpoint() => "/api/species";

        public static string UpdateEndpoint() => "/api/species/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";

        public static string DeleteEndpoint() => "/api/species/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";

        public static string SpeciesSummaryEndpoint() => "/api/species/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/summary";

        public static string SpeciesSummaryWithinDateRangeEndpoint() =>
            "/api/species/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/summary?fromDate=2018-12-01T00:00:00&toDate=2018-12-31T23:59:59";
    }
}