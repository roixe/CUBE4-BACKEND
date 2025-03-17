namespace JamaisASec.Services
{
    public class CacheService<T>(Func<Task<List<T>>> fetchFunc)
    {
        private List<T>? _cache;
        private readonly Func<Task<List<T>>> _fetchFunc = fetchFunc;

        public event Action? CacheUpdated;

        public async Task<List<T>> GetAsync()
        {
            if (_cache == null)
            {
                _cache = await _fetchFunc();
                CacheUpdated?.Invoke();
            }
            return _cache;
        }

        public void ClearCache()
        {
            _cache = null;
        }

        public void RefreshOnEvent(string eventName)
        {
            EventBus.Subscribe(eventName, () => ClearCache());
        }

        public async Task<List<T>> ForceRefreshAsync()
        {
            _cache = await _fetchFunc();
            CacheUpdated?.Invoke();
            return _cache;
        }
    }
}
