namespace Showcase.Core.Exceptions.EmployeeExceptions;

public class EmailAlreadyUsedException(string errorMessage = "Email já registrado!") : MainException(errorMessage);