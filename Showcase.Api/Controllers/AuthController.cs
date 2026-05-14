using Microsoft.AspNetCore.Mvc;
using Showcase.Api.Services.Abstractions;
using Showcase.Core.DTOs.Auth;

namespace Showcase.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(
    IAuthService AuthService,
    ITokenService TokenService,
    IEmailService EmailService
) : ControllerBase
{

    [HttpGet("password-reset")]
    [ProducesResponseType(typeof(VerifyResetTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VerifyResetTokenResponse>> ValidatePasswordResetToken(
        [FromQuery] string token,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(token))
            return NotFound();

        var result = await AuthService
            .ValidatePasswordResetTokenAsync(token, cancellationToken);

        return Ok(result);
    }

    [HttpGet("verify-auth-token")]
    [ProducesResponseType(typeof(VerifyAuthTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<ActionResult<VerifyAuthTokenResponse>> VerifyAuthToken(
        [FromQuery] string token
        )
    {
        var result = TokenService.VerifyAuthToken(token);

        if (result.IsValid)
            return Ok(result);

        return Unauthorized(result);
    }

    [HttpPost("password-reset/confirm")]
    [ProducesResponseType(typeof(ConfirmPasswordTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ConfirmPasswordTokenResponse>> ConfirmPasswordToken(
        [FromQuery] string token,
        [FromBody] VerifyPasswordRequest request,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(token))
            return BadRequest(new ConfirmPasswordTokenResponse { IsValid = false });

        var result = await AuthService
            .ConfirmPasswordTokenAsync(token, request, cancellationToken);

        if (!result.IsValid)
            return BadRequest(new ConfirmPasswordTokenResponse { IsValid = false });

        return Ok(new ConfirmPasswordTokenResponse { IsValid = true });
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var result = await AuthService.LoginAsync(request, cancellationToken);

        if (result is null)
            return Unauthorized();

        return Ok(result);
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(typeof(ForgotPasswordResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ForgotPasswordResponse>> GeneratePasswordResetToken(
        [FromBody] EmailRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var result = await AuthService
            .GeneratePasswordResetTokenAsync(request.Email, cancellationToken);

        if (result is null)
            return BadRequest("Não foi possível gerar o token");

        //Mock email
        await EmailService
            .SendPasswordResetTokenAsync(request.Email, result.Token, cancellationToken);

        return Ok(result);

    }
}