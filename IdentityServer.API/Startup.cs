using Microsoft.IdentityModel.Logging;

namespace Services.Security.IdentityServer;

public class Startup
{
    public IWebHostEnvironment Environment { get; }

    public Startup(IWebHostEnvironment environment)
    {
        Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var builder = services.AddIdentityServer(options =>
        {
            // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
            options.EmitStaticAudienceClaim = true;
        })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients);

        // not recommended for production - you need to store your key material somewhere secure
        builder.AddDeveloperSigningCredential();

        services.AddSingleton<ICorsPolicyService>((container) => {
            var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
            return new DefaultCorsPolicyService(logger)
            {
                AllowedOrigins = { "https://localhost:7137" }
            };
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            IdentityModelEventSource.ShowPII = true;
        }
        app.UseCors("CorsPolicy");
        app.UseIdentityServer();
    }
}
