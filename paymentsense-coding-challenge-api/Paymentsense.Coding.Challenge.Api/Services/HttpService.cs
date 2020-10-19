using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public interface IHttpService
    {
        Task<string> GetAsync(string url, CacheMode mode = CacheMode.UpdateIfExpired);
    }

    public enum CacheMode
    {
        /// <summary>
        /// Do not use the cache
        /// </summary>
        Skip,
        /// <summary>
        /// If expired, updates the cache first and then returns the result. Otherwise returns the cached item immediately. 
        /// </summary>
        UpdateIfExpired
    }

    public class HttpService : IHttpService
    {
        private HttpClient _http;
        private INetworkCache _cache;

        public HttpService(IHttpClientFactory clientFactory, INetworkCache cache)
        {
            _http = clientFactory.CreateClient();
            _cache = cache;
        }

        public async Task<string> GetAsync(string url, CacheMode mode = CacheMode.UpdateIfExpired)
        {
            string result = string.Empty;
            CacheResult cacheData = null;

            // Read cache
            if (mode != CacheMode.Skip)
            {
                cacheData = _cache.GetString(url, TimeSpan.FromDays(1));
                if (cacheData.Exists)
                {
                    result = cacheData.Result;
                }
            }

            // if no result, or cache expired, fetch new data
            if (string.IsNullOrEmpty(result) || (mode == CacheMode.UpdateIfExpired && cacheData.Exists && cacheData.Expired))
            { 
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60));
                using (HttpResponseMessage response = await _http.GetAsync(url, cts.Token).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
            }

            // if we got data, update cache
            if (!string.IsNullOrEmpty(result) && (cacheData == null || cacheData.Expired || !cacheData.Exists) && mode != CacheMode.Skip)
            {
                _cache.Save(url, result);
            }

            return result;
        }
    }
}
