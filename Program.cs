using Net6MVCTemplate.API;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var app = ConfigureBuilder(args).Build();

    ConfigureApp(app);

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

WebApplicationBuilder ConfigureBuilder(string[] strings)
{
    var webApplicationBuilder = WebApplication.CreateBuilder(strings);

    webApplicationBuilder.Host.UseSerilog();

    webApplicationBuilder.Services.Configure<AppConfig>(provider => webApplicationBuilder.Configuration.GetSection("AppConfig").Bind(provider));
    
    var mvcBuilder = webApplicationBuilder.Services.AddControllersWithViews();

    if (webApplicationBuilder.Environment.IsDevelopment())
    {
        mvcBuilder.AddRazorRuntimeCompilation();
    }

    webApplicationBuilder.Services.AddResponseCompression();

    return webApplicationBuilder;
}

void ConfigureApp(WebApplication webApplication)
{
    if (!webApplication.Environment.IsDevelopment())
    {
        webApplication.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

    //SpaServices needs to be registered last 
    if (webApplication.Environment.IsDevelopment())
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