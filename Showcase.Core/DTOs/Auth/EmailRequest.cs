using System.ComponentModel.DataAnnotations;

namespace Showcase.Core.DTOs.Auth;

public class EmailRequest
{
    [Required(ErrorMessage = "Informe o email")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    [MaxLength(256, ErrorMessage = "O email não pode conter mais que 256 caracteres")]
    public string Email { get; set; } = string.Empty;
}
