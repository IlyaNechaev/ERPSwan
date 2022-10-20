using PL.Web.Data;

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

app.Run();
