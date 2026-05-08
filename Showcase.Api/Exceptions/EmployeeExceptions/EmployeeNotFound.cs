namespace Showcase.Api.Exceptions.EmployeeExceptions;

public class EmployeeNotFound(string errorMessage = "Usuário não encontrado!") : MainNotFoundException(errorMessage);