namespace Showcase.Core
{
    public static class ConfigurationClass
    {
        public static string ConnectionString { get; set; } = string.Empty;
        public static string JwtKey { get; set; } = string.Empty;
        public static string JwtIssuer { get; set; } = string.Empty;
        public static string JwtAudience { get; set; } = string.Empty;
        public static string BackendURL { get; set; } = string.Empty;
        public static string FrontEndURL { get; set; } = string.Empty;
    }
}