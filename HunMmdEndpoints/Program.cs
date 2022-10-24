using AspNetCoreRateLimit;
using HunMmdEndpoints.Configurations;
using HunMmdEndpoints.Extensions;
using HunMmdEndpoints.Services;
using HunMmdEndpoints.Services.BackgroundServices;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
//Add Services
builder.Services.AddOptions();
builder.Services.AddMemoryCache();
//load general configuration from appsettings.json
//builder.Services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
//load general configuration from appsettings.json
builder.Services.Configure<ClientRateLimitOptions>(configuration.GetSection("ClientRateLimiting"));

// inject counter and rules stores
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// http client registration
builder.Services.AddHttpClientServices();

builder.Services.AddMMDServices();


// Register LazyCache - makes the IAppCache implementation
builder.Services.AddLazyCache();

builder.Services.AddControllers();

builder.Services.AddHostedService<BackgroundStartupCheckService>();
builder.Services.AddHostedService<LifeCycleCallbackService>();
builder.Services.AddSingleton<StartupHealthCheck>();
builder.Services.AddHealthChecks()
    .AddCheck<StartupHealthCheck>(
        "Startup",
        tags: new[] { "ready" });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.ConfigureExceptionHandler(app.Logger);
}



app.UseClientRateLimiting();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.ConfigureHealthCheck(app.Logger);
app.MapControllers();

app.MapFallbackToFile("index.html"); ;

app.Run();
