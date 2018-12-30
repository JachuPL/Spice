namespace Spice.WebAPI.Tests.Plants.Factories.Events
{
    public static class EndPointFactory
    {
        public static string ListEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events";

        public static string DetailsEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events/DC117408-630E-459B-B16F-DE36EBC58E8F";

        public static string CreateEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events";

        public static string UpdateEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events/DC117408-630E-459B-B16F-DE36EBC58E8F";

        public static string DeleteEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events/DC117408-630E-459B-B16F-DE36EBC58E8F";

        public static string SumTotalEventsEndpoint() => "/api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events/sum";
    }
}