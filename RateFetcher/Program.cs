using RateFetcher.Interfaces;
using RateFetcher.Repositories;
using RateFetcher.Services;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
     
                services.AddSingleton<IConfiguration>(hostContext.Configuration);
                services.AddSingleton<IRateRepository, RateRepository>();
                services.AddSingleton<IRateFetcherService, RateFetcherService>();


                services.AddHostedService<RateFetcherWorker>();
            });
}
