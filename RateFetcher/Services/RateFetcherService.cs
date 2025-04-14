using Newtonsoft.Json;
using RateFetcher.Models;
using RateFetcher.Interfaces;

namespace RateFetcher.Services
{
    public class RateFetcherService : IRateFetcherService
    {
        private readonly HttpClient _httpClient;
        private readonly IRateRepository _rateRepository;
        private readonly string _apiUrl;
        private readonly string _apiKey;

        // הוספת IConfiguration
        public RateFetcherService(IRateRepository rateRepository, IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _rateRepository = rateRepository;
            _apiUrl = configuration["RateApiUrl"];
            _apiKey = configuration["apiKey"];
        }

        public async Task StartFetchingRatesAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await FetchAndUpdateRatesAsync(Currency.USD, cancellationToken);
                    //var eurTask = FetchAndUpdateRatesAsync(Currency.EUR, cancellationToken);
                    //var gbpTask = FetchAndUpdateRatesAsync(Currency.GBP, cancellationToken);

                    //await Task.WhenAll(usdTask, eurTask, gbpTask);

                    Console.WriteLine("Exchange rates updated at " + DateTime.UtcNow);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching rates: " + ex.Message);
                }

                await Task.Delay(10000, cancellationToken);
            }
        }

        private async Task FetchAndUpdateRatesAsync(Currency baseCurrency, CancellationToken cancellationToken)
        {
            var rates = await FetchRatesForCurrencyAsync(baseCurrency, cancellationToken);
            if (rates != null)
            {
                UpdateExchangeRates(baseCurrency, rates);
            }
        }

        private async Task<ExchangeRateResponse> FetchRatesForCurrencyAsync(Currency baseCurrency, CancellationToken cancellationToken)
        {
            try
            {
                var url = $"{_apiUrl}?apikey={_apiKey}&base={baseCurrency.ToString()}";
                var response = await _httpClient.GetStringAsync(url, cancellationToken);
                return JsonConvert.DeserializeObject<ExchangeRateResponse>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching {baseCurrency}: {ex.Message}");
                return null;
            }
        }

        private void UpdateExchangeRates(Currency baseCurrency, ExchangeRateResponse rates)
        {
            if (baseCurrency == Currency.USD)
            {
                _rateRepository.AddOrUpdateRate(Currency.USD, Currency.ILS, rates.Rates["ILS"]);
            }
            else if (baseCurrency == Currency.EUR)
            {
                _rateRepository.AddOrUpdateRate(Currency.EUR, Currency.ILS, rates.Rates["ILS"]);
                _rateRepository.AddOrUpdateRate(Currency.EUR, Currency.ILS, rates.Rates["USD"]);
                _rateRepository.AddOrUpdateRate(Currency.GBP, Currency.EUR, rates.Rates["GBP"]);
            }
            else if (baseCurrency == Currency.GBP)
            {
                _rateRepository.AddOrUpdateRate(Currency.GBP, Currency.ILS, rates.Rates["ILS"]);
            }
        }
    }
}
