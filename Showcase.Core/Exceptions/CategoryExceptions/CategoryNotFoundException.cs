namespace Showcase.Core.Exceptions.CategoryExceptions;

public class CategoryNotFoundException(string errorMessage = "Categoria não encontrada!")
    : MainNotFoundException(errorMessage);


