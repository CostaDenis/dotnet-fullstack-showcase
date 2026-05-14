namespace Showcase.Core.Exceptions.ProductExceptions;

public class ProductNotFoundException(string errorMessage = "Produto não encontrado!")
    : MainNotFoundException(errorMessage);


