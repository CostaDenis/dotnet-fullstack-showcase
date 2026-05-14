using System.Net.Http.Json;
using Showcase.Core.DTOs.Error;
using Showcase.Core.Exceptions.ApiExceptions;

namespace Showcase.Web.Api;

public abstract class BaseApi
{
    protected readonly HttpClient _Http;

    protected BaseApi(HttpClient Http)
    {
        _Http = Http;
    }

    //retorna algo
    protected async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<T>()
                ?? throw new InvalidOperationException("Falha ao desserializar a resposta!");

        var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();

        throw new ApiException
            (error?.StatusCode ?? (int)response.StatusCode, error?.Message ?? "Erro inesperado");
    }

    //nao retorna nada
    protected async Task HandleResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();

            throw new ApiException
                (error?.StatusCode ?? (int)response.StatusCode, error?.Message ?? "Erro inesperado");
        }
    }
}
