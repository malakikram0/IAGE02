using IAGE02.Apps.Operations;
using IAGE02.Components;
using IAGE02.Core.Services.Operations;
using IAGE02.Infrastructures.Storages.Lots;
using IAGE02.Infrastructures.Storages.Operations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<IOperationStorage, OperationStorage>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<ILotStorage, LotStorage>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
