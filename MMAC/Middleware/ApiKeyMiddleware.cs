namespace MMAC.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "ApiKey";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
        {
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;//for unknown user
                await context.Response.WriteAsync("API Key missing!");
                return;
            }
            var apiKey = configuration.GetValue<string>("Authentication:ApiKey");
            if (apiKey != extractedApiKey)
            {
                context.Response.StatusCode = 403; //Access Denied
                await context.Response.WriteAsync("Unauthorized client!");
                return;
            }
            await _next(context);
        }
    }

}
