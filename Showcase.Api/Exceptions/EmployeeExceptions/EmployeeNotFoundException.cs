namespace Showcase.Api.Exceptions.EmployeeExceptions;

public class EmployeeNotFoundException(string errorMessage = "Usuário não encontrado!") : MainNotFoundException(errorMessage);