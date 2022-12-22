using Interview.Angel;
using Interview.Angel.Config;

var builder = WebApplication.CreateBuilder(args);
var host = CreateHostBuilder(args).Build();

builder.Build();
await host.RunAsync();


static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStaticWebAssets();
            webBuilder.UseStartup<Startup>();
        }).ConfigureServices((context, services) =>
        {
            services.Configure<Settings>(context.Configuration.GetSection(Settings.ConfigureSection));
        });
}