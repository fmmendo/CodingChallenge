using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paymentsense.Coding.Challenge.Api.Services;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService service;

        public CountriesController(ICountriesService countriesService)
        {
            service = countriesService;
        }

        [HttpGet]
        public async Task<ActionResult> GetCountries()
        {
            var countries = await service.GetCountriesAsync();

            if (countries == null || countries.Count == 0)
                return StatusCode(500);

            return Ok(countries);
        }
    }
}
