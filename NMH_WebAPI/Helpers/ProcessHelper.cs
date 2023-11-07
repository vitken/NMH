using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Caching.Memory;
using NMH_WebAPI.Models;

namespace NMH_WebAPI.Helpers
{
    public class ProcessHelper : IProcessHelper
    {
        private readonly ILogger<ProcessHelper> _logger;
        private readonly IMemoryCache _cache;
        private readonly string _lastInputDateName = "LastInputDate";
        private readonly int _inputDefaultValue = 2;
        private readonly int _lastUpdateSeconds = 15;

        public ProcessHelper(IMemoryCache cache, ILogger<ProcessHelper> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public OutputModel ProcessKey(int key, double inputValue)
        {
            DateTime lastUpdated = DateTime.MinValue;
            _cache.TryGetValue(_lastInputDateName, out lastUpdated);
            double? computedValue;

            double? previousValue;
            if (!_cache.TryGetValue(key, out previousValue) ||
                (!lastUpdated.Equals(DateTime.MinValue) && DateTime.Compare(lastUpdated.AddSeconds(_lastUpdateSeconds), DateTime.Now) < 0))
            {
                SaveKeyValueToCache(key, _inputDefaultValue);
                computedValue = _inputDefaultValue;
            }
            else
            {
                computedValue = Calculate(inputValue);
                SaveKeyValueToCache(key, computedValue.Value);
            }

            return new OutputModel()
            {
                computed_value = computedValue,
                input_value = inputValue,
                previous_value = previousValue
            };
        }

        private double Calculate(double input)
        {
            return Math.Pow(Math.Log(input), 1/3);
        }

        private void SaveKeyValueToCache(int key, double value)
        {
            _cache.Set(key, value);
            _cache.Set(_lastInputDateName, DateTime.Now);
        }
    }
}
