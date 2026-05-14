namespace Showcase.Core.Exceptions.AuthExceptions;

public class PasswordReuseNotAllowedException(string errorMessage = "A nova senha não pode ser igual à senha antiga!")
    : MainException(errorMessage);