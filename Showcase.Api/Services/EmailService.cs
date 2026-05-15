using Showcase.Api.Services.Abstractions;
using Showcase.Core;

namespace Showcase.Api.Services;

public class EmailService : IEmailService
{
    public async Task SendPasswordResetTokenAsync(string to, string token, CancellationToken cancellationToken = default)
    {
        var url = $"{ConfigurationClass.FrontEndURL}/password-reset?token={Uri.EscapeDataString(token)}";
        var subject = "Redefinir senha - Showcase";
        var body = $@"
        <h2>Redefinir senha - Showcase</h2>
        <p>Para redefinir sua senha, clique no link abaixo:</p>
        <p><a href='{url}' target='_blank'>Redefinir Senha</a></p>
        <p>O link é válido por 15 minutos</p>
        <p>Se você não solicitou a redefinição de senha, ignore este email!</p>";

        await Task.Run(() =>
        {
            Console.WriteLine();
            Console.WriteLine("|---- MOCK DE REDEFINIÇÃO DE SENHA ----|");
            Console.WriteLine($"To: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body:\n{body}");
            Console.WriteLine("|------------------------------------|");
            Console.WriteLine();
        }, cancellationToken);
    }
}
