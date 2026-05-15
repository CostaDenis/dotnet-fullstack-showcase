using Microsoft.AspNetCore.Components;
using Showcase.Core.DTOs.Auth;
using Showcase.Web.Api.Abstractions;
using Showcase.Web.Components;

namespace Showcase.Web.Pages.Auth;

public partial class PasswordResetPage : ComponentBase
{
    [Inject] public IAuthApi AuthApi { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Parameter][SupplyParameterFromQuery] public string? Token { get; set; }
    public VerifyPasswordRequest VerifyPasswordRequest { get; set; } = new();
    public Toast Toast { get; set; } = new();

    // protected async override Task OnAfterRenderAsync(bool firstRender)
    // {
    //     if (!firstRender)
    //         return;

    //     if (string.IsNullOrWhiteSpace(Token))
    //         await ShowMessageAndRedirect("Token inválido!", false);

    //     var response = await AuthApi.ValidatePasswordResetTokenAsync(Token!);

    //     if (!response.IsValid)
    //         await ShowMessageAndRedirect("Token inválido!", false);

    // }

    protected async override Task OnParametersSetAsync()
    {
        if (string.IsNullOrWhiteSpace(Token))
            await ShowMessageAndRedirect("Token inválido!", false);

        var response = await AuthApi.ValidatePasswordResetTokenAsync(Token!);

        if (!response.IsValid)
            await ShowMessageAndRedirect("Token inválido!", false);
    }

    protected async Task ShowMessageAndRedirect(string message, bool result)
    {
        await Toast.ShowToastAsync(message, result);
        StateHasChanged();
        await Task.Yield();

        if (!result)
            NavigationManager.NavigateTo("/forgot-password", true);

        NavigationManager.NavigateTo("/", true);
    }

    protected async Task HandlePasswordResetAsync()
    {
        var response = await AuthApi.ConfirmPasswordTokenAsync(Token!, VerifyPasswordRequest);

        if (response is null ||
                response.IsValid == false)
            await ShowMessageAndRedirect("Token inválido", false);

        await ShowMessageAndRedirect("Senha alterada com sucesso! Redirecionando para login...", false);
    }
}