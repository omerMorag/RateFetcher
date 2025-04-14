using RatePrinter.Models;

namespace RatePrinter.Interfaces
{
    public interface IRateRepository
    {
        Dictionary<string, ExchangeRate> GetAllRates();
        ExchangeRate? GetRateByPair(Currency fromCurrency, Currency toCurrency);
    }
}
