namespace Showcase.Api.Exceptions.CategoryExceptions;

public class CategoryNotFoundException(string errorMessage = "Categoria não encontrada!")
    : MainNotFoundException(errorMessage);


