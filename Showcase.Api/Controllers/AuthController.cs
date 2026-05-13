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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> VerifyResetPasswordToken(
        [FromQuery] string token,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(token))
            return NotFound();

        var isValid = await AuthService
            .ValidatePasswordResetTokenAsync(token, cancellationToken);

        return Ok();
    }

    [HttpGet("verify-auth-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<bool> VerifyAuthToken(
        [FromQuery] string token
        )
    {
        var result = TokenService.VerifyAuthToken(token);

        if (result)
            return Ok(result);

        return Unauthorized(result);
    }

    [HttpPost("password-reset/confirm")]
    [ProducesResponseType(typeof(ResetPasswordResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ResetPasswordResponse>> ResetPasswordAsync(
        [FromQuery] string token,
        [FromBody] VerifyPasswordRequest request,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(token))
            return BadRequest(new ResetPasswordResponse { Result = false });

        var success = await AuthService
            .ConfirmPasswordTokenAsync(token, request, cancellationToken);

        if (!success)
            return BadRequest(new ResetPasswordResponse { Result = false });

        return Ok(new ResetPasswordResponse { Result = true });
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
    public async Task<ActionResult<ForgotPasswordResponse>> ForgotPassword(
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
            .SendPasswordResetTokenAsync(request.Email, result, cancellationToken);

        return Ok(new ForgotPasswordResponse
        {
            Token = result
        });

    }
}