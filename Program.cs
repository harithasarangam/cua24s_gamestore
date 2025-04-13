using GameStore.Services;
using Microsoft.ApplicationInsights;

var builder = WebApplication.CreateBuilder(args);

// Lägg till tjänster för MVC och GameService
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<GameService>();
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();
var telemetryClient = app.Services.GetRequiredService<TelemetryClient>();
telemetryClient.TrackTrace("🚀 Application has started!");

// Konfigurera HTTP-anrop
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Games}/{action=Index}/{id?}");

app.Run();
