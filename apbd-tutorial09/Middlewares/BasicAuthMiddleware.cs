using System.Net.Http.Headers;
using System.Text;
using apbd_tutorial09.Helpers;

namespace apbd_tutorial09.Middlewares;

public class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<BasicAuthMiddleware> _logger;
    private readonly AppDbContext _dbContext;

    public BasicAuthMiddleware(
        RequestDelegate next,
        ILogger<BasicAuthMiddleware> logger,
        AppDbContext dbContext)
    {
        _next = next;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);

            if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && authHeader.Parameter != null)
            {
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(":");

                var username = credentials[0];
                var password = credentials[1];

                if (IsAuthorized(username, password))
                {
                    await _next(context);
                    return;
                }
            }
        }
        context.Response.Headers["WWW-Authenticate"] = "Basic";
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized access.");
    }

    private bool IsAuthorized(string username, string password)
    {
        return username == "admin" && password == "password";
    }
}