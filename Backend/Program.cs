
using BuildingBlocks;
using Infrastructure.Configuration;
using LINTest;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Manually configure Configuration object
var configuration = new ConfigurationBuilder()
    
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Add services to the container.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddCore();

builder.Services.AddActuatorServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddLINTestServices();



var app = builder.Build();


//External Services adds here
// builder.Services.AddLINTestServices();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Use services from the service provider
// using var scope = app.Services.CreateScope();
// var services = scope.ServiceProvider;
// var context = services.GetRequiredService<ApplicationDbContext>();
// var dataService = services.GetRequiredService<DataHandlingService>();
//
// var filePath = "C:\\Users\\Administrator\\Desktop\\inputFiles\\firstFile.csv";
// try
// {
//     var csvModel = CSVHandler.ReadCSV(filePath);
//     dataService.SaveData(csvModel);
//     Console.WriteLine("Data saved successfully.");
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"An error occurred while saving the data: {ex.Message}");
// }


app.Run();