namespace Showcase.Core.Exceptions.EmployeeExceptions;

public class EmployeeNotFoundException(string errorMessage = "Usuário não encontrado!") : MainNotFoundException(errorMessage);