namespace Spice.WebAPI.Tests.Nutrients.Factories
{
    public static class EndPointFactory
    {
        public static string ListEndpoint() => "/api/nutrients";

        public static string DetailsEndpoint() => "/api/nutrients/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";

        public static string CreateEndpoint() => "/api/nutrients";

        public static string UpdateEndpoint() => "/api/nutrients/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";

        public static string DeleteEndpoint() => "/api/nutrients/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";
    }
}