using RatePrinter.Interfaces;
using RatePrinter.Models;
using RatePrinter.Interfaces;
namespace RatePrinter.Services
{
    public class RatePrinterService : IRatePrinterService
    {
        private readonly IRateRepository _rateRepository;

        public RatePrinterService(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        public Dictionary<string, ExchangeRate> GetAllRates()
        {
            try
            {
                return _rateRepository.GetAllRates();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all exchange rates: {ex.Message}");
                return new Dictionary<string, ExchangeRate>();
            }
        }

        public ExchangeRate? GetRateByPair(Currency fromCurrency, Currency toCurrency)
        {
            try
            {
                return _rateRepository.GetRateByPair(fromCurrency, toCurrency);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving exchange rate for pair {fromCurrency.ToString()},{toCurrency.ToString()}: {ex.Message}");
                return null;
            }
        }
    }
}

