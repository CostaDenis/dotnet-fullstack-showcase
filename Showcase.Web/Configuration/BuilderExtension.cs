using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Showcase.Core;
using Showcase.Web.Api;
using Showcase.Web.Api.Abstractions;

namespace Showcase.Web.Configuration;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebAssemblyHostBuilder builder)
    {
        ConfigurationClass.BackendURL = builder.Configuration.GetValue<string>("BackendUrl")
          ?? throw new InvalidOperationException("URL do Backend não encontrada!");
    }

    public static void AddServices(this WebAssemblyHostBuilder builder)
    {
        //api
        builder.Services.AddHttpClient<IAuthApi, AuthApi>(client =>
        {
            client.BaseAddress = new Uri(ConfigurationClass.BackendURL);
        });
    }
}