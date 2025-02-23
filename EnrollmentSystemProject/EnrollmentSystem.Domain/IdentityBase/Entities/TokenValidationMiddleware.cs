using System.Security.Claims;
using EnrollmentSystem.Domain.IdentityBase.Repositories;
using EnrollmentSystem.Utils.Exceptions;
using Microsoft.AspNetCore.Http;

namespace EnrollmentSystem.Domain.IdentityBase.Entities;

public class TokenValidationMiddleware : IMiddleware
{
    private readonly TokenMemoryRepository _tokenPersistency;
    private readonly string[] _rolesConcorrentes = new string[] { };


    public TokenValidationMiddleware(TokenMemoryRepository tokenPersistency)
    {
        _tokenPersistency = tokenPersistency;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (UsuarioNaoAutenticado(context) || UsuarioComPermissaoConcorrencia(context))
        {
            await next.Invoke(context);
            return;
        }

        if (UsuarioSemPermissaoConcorrencia(context))
        {
            new UnauthorizedException("Essa Sessão Expirou devido a um novo login.");
        }

        await next.Invoke(context);
    }

    private static bool UsuarioNaoAutenticado(HttpContext context)
    {
        return !context.User.Identity?.IsAuthenticated ?? false;
    }

    public bool UsuarioSemPermissaoConcorrencia(HttpContext context)
    {
        var IatCorrente = context.User.FindFirst("iat")?.Value;
        var userId = context.User.Claims.First(c => c.Type == "userid")?.Value;

        if (!_tokenPersistency.TryGet(userId, out string ultimoIat)) throw new UnauthorizedException("Não foi possivel verificar usuario");

        return IatCorrente != ultimoIat;
    }

    public bool UsuarioComPermissaoConcorrencia(HttpContext context)
    {
        var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
        return _rolesConcorrentes.Any(r => role == r);
    }
}