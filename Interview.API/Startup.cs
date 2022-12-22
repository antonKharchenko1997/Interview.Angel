using Interview.Angel.Config;
using Microsoft.Extensions.Options;

namespace Interview.Angel;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHostedService<Worker>();
        services.AddSingleton(provider =>
        {
          var settings =  provider.GetService<IOptions<Settings>>();
          return new Core.Angel(settings!.Value.TelegramBotToken, settings.Value.TwitchApiAccessToken, settings.Value.TwitchApiClientId);
        });
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        
    }
}