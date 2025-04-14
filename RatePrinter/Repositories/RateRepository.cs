using RatePrinter.Models;
using RatePrinter.Interfaces;
using Newtonsoft.Json;

namespace RatePrinter.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Data", "exchangeRates.json");

        public RateRepository()
        {
            if (!File.Exists(_filePath))
            {
                var initialContent = "{}";
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
                File.WriteAllText(_filePath, initialContent);
            }
        }

        public Dictionary<string, ExchangeRate> GetAllRates()
        {
            return LoadRatesFromFile();
        }

        public ExchangeRate? GetRateByPair(Currency fromCurrency, Currency toCurrency)
        {
            var pair = $"{fromCurrency.ToString()}/{toCurrency.ToString()}";
            var rates = LoadRatesFromFile();

            if (rates.TryGetValue(pair, out var rate))
            {
                return rate;
            }

            return null;
        }

        private Dictionary<string, ExchangeRate> LoadRatesFromFile()
        {
            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<Dictionary<string, ExchangeRate>>(jsonData) ?? new Dictionary<string, ExchangeRate>();
        }
    }
}
