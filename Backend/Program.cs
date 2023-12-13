using BuildingBlocks;
using BuildingBlocks.Application;
using BuildingBlocks.Integration.Inbox.Configuration;
using Infrastructure.Configuration;
using LINTest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TECHLineService;
using TestResult.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Add services to the container.

builder.Services.AddCore(builder.Configuration);
builder.Services.AddLINTestServices(builder.Configuration);
builder.Services.AddInbox();
builder.Services.AddActuatorServices();
builder.Services.AddTestResultServices();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddTECHLineServices();

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