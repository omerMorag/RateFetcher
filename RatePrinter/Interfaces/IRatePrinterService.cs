using RatePrinter.Models;

namespace RatePrinter.Interfaces
{
    public interface IRatePrinterService
    {
        Dictionary<string, ExchangeRate> GetAllRates();
        ExchangeRate? GetRateByPair(Currency fromCurrency, Currency toCurrency);
    }
}

