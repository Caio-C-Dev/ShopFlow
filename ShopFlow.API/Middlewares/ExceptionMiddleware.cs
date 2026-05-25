using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ShopFlow.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors
                .Select(e => new { field = e.PropertyName, message = e.ErrorMessage });

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new { errors }));
        }
        catch (Exception)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new { error = "Erro interno do servidor." }));
        }
    }
}