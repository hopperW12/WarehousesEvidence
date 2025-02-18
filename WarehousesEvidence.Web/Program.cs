using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;
using WarehousesEvidence.App.Extensions;
using WarehousesEvidence.Data;
using WarehousesEvidence.Data.Extensions;
using WarehousesEvidence.Web.Components;
using WarehousesEvidence.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
});

builder.Services.AddDatabase();

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddModelMappers();

var app = builder.Build();

// Database
app.AddDatabase();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();