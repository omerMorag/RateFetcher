using RateFetcher.Models;

namespace RateFetcher.Interfaces
{
    public interface IRateRepository
    {
        public void AddOrUpdateRate(Currency fromCurrency, Currency toCurrency, double rate);
    }
}
