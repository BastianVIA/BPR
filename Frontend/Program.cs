using Frontend.Service.AlertService;
using Radzen;
using Frontend.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAlertService, AlertService>();

builder.Services.SetupBackendConnection();
await builder.Services.AddValidationSettings();
builder.Services.AddModels();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRadzenComponents();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();