using Microsoft.AspNetCore.Components;
using Showcase.Core.DTOs.Auth;
using Showcase.Web.Api.Abstractions;
using Showcase.Web.Components;

namespace Showcase.Web.Pages.Auth;

public partial class ForgotPasswordPage : ComponentBase
{
    [Inject] public IAuthApi AuthApi { get; set; } = null!;
    public EmailRequest EmailRequest { get; set; } = new();
    public Toast Toast { get; set; } = new();

    protected async Task HandleForgotPasswordAsync()
    {
        await AuthApi.GeneratePasswordResetTokenAsync(EmailRequest);
        await Toast.ShowToastAsync("Caso o email existir, será enviado informações adicionais a ele!", true);
    }
}