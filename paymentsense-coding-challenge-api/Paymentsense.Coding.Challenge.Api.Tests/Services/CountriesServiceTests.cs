using FluentAssertions;
using Moq;
using Paymentsense.Coding.Challenge.Api.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class CountriesServiceTests
    {
        Mock<IHttpService> http = new Mock<IHttpService>();
        CountriesService service;

        public CountriesServiceTests()
        {
            service = new CountriesService(http.Object);
        }

        [Fact]
        public async Task GetCountries_ReturnsListOfCountries()
        {
            string json = "[{\"name\":\"Afghanistan\",\"topLevelDomain\":[\".af\"],\"alpha2Code\":\"AF\",\"alpha3Code\":\"AFG\",\"callingCodes\":[\"93\"],\"capital\":\"Kabul\",\"altSpellings\":[\"AF\",\"Afġānistān\"],\"region\":\"Asia\",\"subregion\":\"Southern Asia\",\"population\":27657145,\"latlng\":[33.0,65.0],\"demonym\":\"Afghan\",\"area\":652230.0,\"gini\":27.8,\"timezones\":[\"UTC+04:30\"],\"borders\":[\"IRN\",\"PAK\",\"TKM\",\"UZB\",\"TJK\",\"CHN\"],\"nativeName\":\"افغانستان\",\"numericCode\":\"004\",\"currencies\":[{\"code\":\"AFN\",\"name\":\"Afghan afghani\",\"symbol\":\"؋\"}],\"languages\":[{\"iso639_1\":\"ps\",\"iso639_2\":\"pus\",\"name\":\"Pashto\",\"nativeName\":\"پښتو\"},{\"iso639_1\":\"uz\",\"iso639_2\":\"uzb\",\"name\":\"Uzbek\",\"nativeName\":\"Oʻzbek\"},{\"iso639_1\":\"tk\",\"iso639_2\":\"tuk\",\"name\":\"Turkmen\",\"nativeName\":\"Türkmen\"}],\"translations\":{\"de\":\"Afghanistan\",\"es\":\"Afganistán\",\"fr\":\"Afghanistan\",\"ja\":\"アフガニスタン\",\"it\":\"Afghanistan\",\"br\":\"Afeganistão\",\"pt\":\"Afeganistão\",\"nl\":\"Afghanistan\",\"hr\":\"Afganistan\",\"fa\":\"افغانستان\"},\"flag\":\"https://restcountries.eu/data/afg.svg\",\"regionalBlocs\":[{\"acronym\":\"SAARC\",\"name\":\"South Asian Association for Regional Cooperation\",\"otherAcronyms\":[],\"otherNames\":[]}],\"cioc\":\"AFG\"}]";

            http.Setup(_ => _.GetAsync(It.IsAny<string>(), CacheMode.UpdateIfExpired))
                .Returns(Task.FromResult(json));

            var result = await service.GetCountriesAsync();

            result.Count.Should().Be(1);
            result[0].Name.Should().Be("Afghanistan");
        }

        [Fact]
        public async Task GetCountries_ReturnsNullIfEmptyResponse()
        {
            http.Setup(_ => _.GetAsync(It.IsAny<string>(), CacheMode.UpdateIfExpired))
                .Returns(Task.FromResult(string.Empty));

            var result = await service.GetCountriesAsync();

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCountries_ReturnsNullIfInvalidResponse()
        {
            string notjson = "uh oh";

            http.Setup(_ => _.GetAsync(It.IsAny<string>(), CacheMode.UpdateIfExpired))
                .Returns(Task.FromResult(notjson));

            var result = await service.GetCountriesAsync();

            result.Should().BeNull();
        }
    }
}
