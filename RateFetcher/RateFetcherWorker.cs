using Microsoft.Extensions.Hosting;
using RateFetcher.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RateFetcher.Services
{
    public class RateFetcherWorker : BackgroundService
    {
        private readonly IRateFetcherService _rateFetcherService;

        public RateFetcherWorker(IRateFetcherService rateFetcherService)
        {
            _rateFetcherService = rateFetcherService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _rateFetcherService.StartFetchingRatesAsync(stoppingToken);
                    Console.WriteLine("Exchange rates updated.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in fetching rates: {ex.Message}");
                }

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
