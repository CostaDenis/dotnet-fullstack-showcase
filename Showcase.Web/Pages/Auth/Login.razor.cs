using Microsoft.AspNetCore.Components;
using Showcase.Core.DTOs.Auth;
using Showcase.Web.Api.Abstractions;
using Showcase.Web.Components;

namespace Showcase.Web.Pages.Auth;

public partial class LoginPage : ComponentBase
{
    [Inject] IAuthApi AuthApi { get; set; } = null!;
    public LoginRequest LoginRequest { get; set; } = new();
    public Toast Toast { get; set; } = new();

    protected async Task HandleLoginAsync()
    {
        var result = await AuthApi.LoginAsync(LoginRequest);

        if (result is null)
        {
            await Toast.ShowToastAsync("Credenciais inválidas!", false);
            return;
        }

        await Toast.ShowToastAsync("Login realizado com sucesso", true);
    }
}