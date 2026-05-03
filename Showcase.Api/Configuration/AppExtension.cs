using Showcase.Api.Middlewares;

namespace Showcase.Api.Configuration
{
    public static class AppExtension
    {
        public static void OnConfigureDevEnvironment(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapSwagger().RequireAuthorization();
        }

        public static void UseMiddleware(this WebApplication app)
            => app.UseMiddleware<ExceptionMiddleware>();

        public static void AddSecurity(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}