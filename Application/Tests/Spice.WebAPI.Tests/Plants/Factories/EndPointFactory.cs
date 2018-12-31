namespace Spice.WebAPI.Tests.Plants.Factories
{
    internal static class EndPointFactory
    {
        public static string ListEndpoint() => "/api/plants";

        public static string DetailsEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";

        public static string CreateEndpoint() => "/api/plants";

        public static string UpdateEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";

        public static string DeleteEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";
    }
}