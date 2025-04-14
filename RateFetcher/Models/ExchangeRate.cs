using System;

namespace RateFetcher.Models
{
    public class ExchangeRate
    {
        public string Pair { get; set; }
        public double Rate { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public ExchangeRate(string pair, double rate, DateTime lastUpdateTime)
        {
            Pair = pair;
            Rate = rate;
            LastUpdateTime = lastUpdateTime;
        }
    }
 
}
