using AsteroidTracker.WebApi.Service;
using AsteroidTracker.WebApi.Utils;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Configuration>(builder.Configuration.GetSection("Configuration"));
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<NasaApiService>();

builder.Services.AddCors(o => o.AddPolicy(MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:7283")
                      .AllowAnyMethod()
                       .AllowAnyHeader();
                      }));


var app = builder.Build();

app.UseMiddleware<LoginMiddleware>();
app.UseCors(MyAllowSpecificOrigins);
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
