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
        Task<List<Country>> Get();
    }

    public class CountriesService : ICountriesService
    {
        private readonly HttpClient _http;

        public CountriesService(IHttpClientFactory clientFactory)
        {
            _http = clientFactory.CreateClient();
        }

        public async Task<List<Country>> Get()
        {
            RestCountriesResponse countriesResponse = new RestCountriesResponse();

            using (HttpResponseMessage response = await _http.GetAsync("https://restcountries.eu/rest/v2/all").ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    countriesResponse.Countries = JsonSerializer.Deserialize<List<Country>>(json);
                }
            }

            return countriesResponse.Countries;
        }
    }
}
