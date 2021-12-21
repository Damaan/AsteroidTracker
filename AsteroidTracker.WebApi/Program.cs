using AsteroidTracker.WebApi.Service;
using AsteroidTracker.WebApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Configuration>(builder.Configuration.GetSection("Configuration"));
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<NasaApiService>();

var app = builder.Build();

app.UseMiddleware<LoginMiddleware>();

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
