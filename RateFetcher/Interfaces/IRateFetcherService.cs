namespace RateFetcher.Interfaces
{
    public interface IRateFetcherService
    {
        public Task StartFetchingRatesAsync(CancellationToken cancellationToken);
    }
}