using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paymentsense.Coding.Challenge.Api.Models;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public interface ICountriesService
    {
        Task<List<Country>> GetCountriesAsync();
    }

    public class CountriesService : ICountriesService
    {
        private readonly IHttpService _http;

        public CountriesService(IHttpService httpService)
        {
            _http = httpService;
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            string response = await _http.GetAsync("https://restcountries.eu/rest/v2/all").ConfigureAwait(false);

            if (string.IsNullOrEmpty(response))
                return null;

            List<Country> countries;
            try
            {
                countries = JsonSerializer.Deserialize<List<Country>>(response);
            }
            catch (JsonException)
            {
                return null;
            }

            return countries;
        }
    }
}
