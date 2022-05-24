using Polaris.NetCoreMVCTemplate.WebHost;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        new JsonFormatter(renderMessage: true),
        Path.Combine(AppContext.BaseDirectory, "logs//Serilog.json"),
        shared: true,
        fileSizeLimitBytes: 20_971_520,
        rollOnFileSizeLimit: true,
        retainedFileCountLimit: 10)
    .CreateLogger();

try
{
    var app = ConfigureBuilder(args, out var isDevelopment).Build();

    ConfigureApp(app, isDevelopment);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

#region Bootstrapping Methods

WebApplicationBuilder ConfigureBuilder(string[] strings, out bool isDevelopment)
{
    var webApplicationBuilder = WebApplication.CreateBuilder(strings);

    isDevelopment = webApplicationBuilder.Environment.IsEnvironment("Local") || webApplicationBuilder.Environment.IsDevelopment();

    webApplicationBuilder.Host.UseSerilog();

    var mvcBuilder = webApplicationBuilder.Services.AddControllersWithViews();

    if (isDevelopment)
    {
        mvcBuilder.AddRazorRuntimeCompilation();
    }

    webApplicationBuilder.Services.AddResponseCompression();

    ServiceRegistry.AddConfig(webApplicationBuilder.Services, webApplicationBuilder.Configuration);

    ServiceRegistry.AddRestClients(webApplicationBuilder.Services, webApplicationBuilder.Configuration);

    return webApplicationBuilder;
}

void ConfigureApp(WebApplication webApplication, bool isDevelopment)
{
    if (!isDevelopment)
    {
        webApplication.UseExceptionHandler("/Home/Error");
        webApplication.UseHsts();
    }

    webApplication.UseSerilogRequestLogging();

    webApplication.UseStaticFiles();

    webApplication.UseHttpsRedirection();

    webApplication.UseRouting();

    webApplication.UseAuthorization();

    webApplication.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapFallbackToController("Index", "Home");
    });

    webApplication.UseResponseCompression();
    
    if (isDevelopment)
    {
        webApplication.Use(async (context, next) =>
        {
            context.Response.Headers.Add("Cache-Control", "private, no-cache");
            await next.Invoke();
        });
        webApplication.UseDeveloperExceptionPage();
    }
}

#endregion