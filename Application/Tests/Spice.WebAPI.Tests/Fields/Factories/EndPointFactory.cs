namespace Spice.WebAPI.Tests.Fields.Factories
{
    public static class EndPointFactory
    {
        public static string ListEndpoint() => "/api/fields";

        public static string DetailsEndpoint() => "/api/fields/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";

        public static string CreateEndpoint() => "/api/fields";

        public static string UpdateEndpoint() => "/api/fields/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";

        public static string DeleteEndpoint() => "/api/fields/F3694C70-AC96-4BBC-9D70-7C1AF728E93F";
    }
}