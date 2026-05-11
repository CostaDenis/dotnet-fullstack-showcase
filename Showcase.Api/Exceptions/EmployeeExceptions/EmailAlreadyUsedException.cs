namespace Showcase.Api.Exceptions.EmployeeExceptions;

public class EmailAlreadyUsedException(string errorMessage = "Email já registrado!") : MainException(errorMessage);