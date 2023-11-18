using Application;
using Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging
    .ClearProviders();

builder.Host.UseSerilog(
    (_, config) => config.ReadFrom.Configuration(builder.Configuration),
    writeToProviders: true);

// Add services to the container.
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/api/error");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
