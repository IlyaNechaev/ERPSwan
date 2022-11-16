namespace ES.Web.Contracts.V1;

public static class ApiRoutes
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = $"{Root}/{Version}";

    public static class Home
    {
        public const string Login = $"{Base}/login";

        public const string Register = $"{Base}/register";

        public const string Test = $"{Base}/test";
    }
}
