namespace Showcase.Api.Exceptions;

public class MainNotFoundException(string errorMessage) : Exception(errorMessage);