using Microsoft.Extensions.Caching.Memory;

namespace ReportsManagements.Services
{
    public class UploadRateLimiterService
    {
        private readonly IMemoryCache _cache;
        private const int LIMIT = 20;
        private static readonly TimeSpan PERIOD = TimeSpan.FromMinutes(1);

        public UploadRateLimiterService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool CanUpload(string key)
        {
            if (_cache.TryGetValue<int>(key, out var count))
            {
                if (count >= LIMIT) return false;

                _cache.Set(key, count + 1, PERIOD);
            }
            else
            {
                _cache.Set(key, 1, PERIOD);
            }

            return true;
        }
    }
}
