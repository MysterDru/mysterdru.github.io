namespace mysterdru.github.io;

internal static class AppBuilderExtensions
{
    public static IApplicationBuilder MapRedirect(this IApplicationBuilder builder, string requestedPath,
        string redirectPath)
    {
        builder.UseWhen((context => context.Request.Path == requestedPath), appBuilder =>
        {
            appBuilder.Use((HttpContext httpContext, Func<Task> next) =>
            {
                httpContext.Response.Redirect(redirectPath);

                return Task.CompletedTask;
            });
        });

        return builder;
    }
}