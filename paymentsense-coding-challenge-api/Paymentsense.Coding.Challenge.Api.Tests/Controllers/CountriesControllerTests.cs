using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class CountriesControllerTests
    {
        Mock<ICountriesService> service = new Mock<ICountriesService>();
        CountriesController controller;

        public CountriesControllerTests()
        {
            controller = new CountriesController(service.Object);
        }

        [Fact]
        public void Get_ReturnsListOfCountries()
        {
            service.Setup(_ => _.GetCountriesAsync())
                   .Returns(Task.FromResult(new List<Country>() { new Country() }));

            var result = controller.GetCountries().Result as OkObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeOfType<List<Country>>();
        }

        [Fact]
        public void Get_ReturnsServerError()
        {
            service.Setup(_ => _.GetCountriesAsync())
                   .Returns(Task.FromResult<List<Country>>(null));

            var result = controller.GetCountries().Result as StatusCodeResult;

            result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
