using ES.Web.Middleware;
using ES.Web.Data;
using ES.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Добавляем собственные сервисы
#region CUSTOM_SERVICES

// Entity Framework Core
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddSqlDbContext(builder.Configuration);

builder.Services.AddTransient<SignInManager, SignInManager>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISecurityService, SecurityService>();
builder.Services.AddTransient<IJwtUtils, JwtUtils>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<JwtMiddleware>();

app.Run();
