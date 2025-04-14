using RateFetcher.Models;
using RateFetcher.Interfaces;
using Newtonsoft.Json;

namespace RateFetcher.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Data", "exchangeRates.json");

        public RateRepository()
        {
            if (!File.Exists(_filePath))
            {
                var initialContent = "{}";
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
                File.WriteAllText(_filePath, initialContent);
            }
        }

        public void AddOrUpdateRate(Currency fromCurrency, Currency toCurrency, double rate)
        {
            var pair = $"{fromCurrency}/{toCurrency}";
            var exchangeRate = new ExchangeRate(pair, rate, DateTime.UtcNow);

            var rates = LoadRatesFromFile();

            rates[pair] = exchangeRate;

            SaveRatesToFile(rates);
        }

        private Dictionary<string, ExchangeRate> LoadRatesFromFile()
        {
            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<Dictionary<string, ExchangeRate>>(jsonData) ?? new Dictionary<string, ExchangeRate>();
        }

        private void SaveRatesToFile(Dictionary<string, ExchangeRate> rates)
        {
            var jsonData = JsonConvert.SerializeObject(rates, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }
    }
}
