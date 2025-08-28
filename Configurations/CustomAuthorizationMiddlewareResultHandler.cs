using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace LicencaApi.Configurations
{
    public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
           var userName = context.User?.Identity?.Name;
           
            if (authorizeResult.Forbidden)
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;


                await context.Response.WriteAsync($"Usuário, {userName} . Sem permissão de acesso !.");
                return;
            }

            if (authorizeResult.Challenged)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync($"Usuário, {userName} . Não autenticado !.");
                return;
            }

            if (authorizeResult.Forbidden)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync($"Usuário, {userName} . Acesso negado !.");
                return;
            }
            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
