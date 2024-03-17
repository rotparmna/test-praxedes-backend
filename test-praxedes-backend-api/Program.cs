using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Filters;
using test_praxedes_backend_api.Infraestructure;
using test_praxedes_backend_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(LogFilter));
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Test Praxedes Api",
        Version = "v1",
        Description = "Test backend Praxedes Api"
    });

    options.AddSecurityDefinition("oath2ClientCredentials", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            ClientCredentials = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri($"{builder.Configuration.GetValue<string>("IdentityUrl")}/connect/token"),
                Scopes = new Dictionary<string, string>
                        {
                            { "PraxedesBackendUser", "Praxedes Backend User" },
                            { "PraxedesBackendPost", "Praxedes Backend Post"}
                        }
            }
        }
    });
    options.OperationFilter<AuthorizeCheckOperationFilter>();

});

builder.Services.AddSingleton(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                            throw new ApplicationException("Connection string is null");
    return new SqlConnectionFactory(connectionString);
});
builder.Services.AddTransient<IFamilyGroupService, FamilyGroupService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISpGetFamilyGroup, SpGetFamilyGroup>();
builder.Services.AddTransient<ISpGetUser, SpGetUser>();
builder.Services.AddTransient<ISpInsertUser, SpInsertUser>();
builder.Services.AddTransient<ISpInsertUserRelationship, SpInsertUserRelationship>();
builder.Services.AddTransient<ISpDeleteUser, SpDeleteUser>();
builder.Services.AddTransient<ISpGetUserById, SpGetUserById>();
builder.Services.AddTransient<ISpGetUsers, SpGetUsers>();
builder.Services.AddTransient<ISpUpdateUser, SpUpdateUser>();
builder.Services.AddTransient<ISpInsertActivityApi, SpInsertActivityApi>();
builder.Services.AddTransient<ISpUpdateActivityApi, SpUpdateActivityApi>();
builder.Services.AddTransient<IActivityApiService, ActivityApiService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<ISpInsertPost, SpInsertPost>();
builder.Services.AddTransient<ISpUpdatePost, SpUpdatePost>();
builder.Services.AddTransient<ISpDeletePost, SpDeletePost>();
builder.Services.AddTransient<ISpGetPostById, SpGetPostById>();
builder.Services.AddTransient<ISpGetPosts, SpGetPosts>();

builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
                options.Audience = "PraxedesBackendUser";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "PraxedesBackendUser");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization("ApiScope");

app.Run();
