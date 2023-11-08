using BuildingBlocks;
using BuildingBlocks.Application;
using Infrastructure.Configuration;
using LINTest;
using LINTest.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Add services to the container.

builder.Services.AddCore(builder.Configuration);

builder.Services.AddActuatorServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddLINTestServices(builder.Configuration);

builder.Services.Configure<FileProcessorOptions>(builder.Configuration.GetSection("FileProcessor"));

builder.Services.Configure<StateManagerOptions>(builder.Configuration.GetSection("StateManager"));

var app = builder.Build();

app.UseMiddleware<GlobalExceptionFilter>();

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