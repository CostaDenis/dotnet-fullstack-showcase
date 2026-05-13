using System.ComponentModel.DataAnnotations;

namespace Showcase.Core.DTOs.Auth;

public class VerifyPasswordRequest
{
    [Required(ErrorMessage = "Informe a senha")]
    [MinLength(3, ErrorMessage = "A senha deve conter mais que 8 caracteres")]
    [MaxLength(30, ErrorMessage = "A senha não pode conter mais que 30 caracteres")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma minúscula, um número e um símbolo.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirme a senha")]
    [Compare("Password", ErrorMessage = "As senhas não coincidem")]
    public string ConfirmationPassword { get; set; } = string.Empty;
}
