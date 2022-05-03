namespace AgileBoard.Api.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class UserStories
        {
            public const string GetAll = Base + "/userstories";

            public const string Get = Base + "/userstories/{userStoryId}";

            public const string Update = Base + "/userstories/{userStoryId}";

            public const string PatchUpdate = Base + "/userstories/{userStoryId}";

            public const string Create = Base + "/userstories";
            
            public const string Delete = Base + "/userstories/{userStoryId}";
        }
    }
}
