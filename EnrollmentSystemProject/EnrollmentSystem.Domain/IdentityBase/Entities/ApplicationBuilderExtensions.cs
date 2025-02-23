using EnrollmentSystem.Domain.IdentityBase.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace EnrollmentSystem.Domain.IdentityBase.Entities;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseConfiguracaoSessao(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ConfiguracaoSessaoMiddleware>();

    }

    public class ConfiguracaoSessaoMiddleware
    {
        private readonly RequestDelegate _proximo;

        public ConfiguracaoSessaoMiddleware(RequestDelegate next)
        {
            _proximo = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserSession session)
        {
            if (context.User.Identities.Any(id => id.IsAuthenticated))
            {
                session.UserId = context.User.Claims.FirstOrDefault(x => x.Type == "userid").Value;
                session.UserName = context.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
                session.Roles = context.User.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(x => x.Value).ToList();
            }

            await _proximo.Invoke(context);
        }

    }
}