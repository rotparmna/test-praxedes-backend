using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Infraestructure;
using test_praxedes_backend_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

