namespace Showcase.Api.Exceptions.EmployeeExceptions;

public class EmailAlreadyUsed(string errorMessage = "Email já registrado!") : MainException(errorMessage);