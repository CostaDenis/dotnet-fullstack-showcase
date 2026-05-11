namespace Showcase.Api.Exceptions.ProductExceptions;

public class ProductNotFoundException(string errorMessage = "Produto não encontrado!")
    : MainNotFoundException(errorMessage);


