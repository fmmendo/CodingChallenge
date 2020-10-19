using FluentAssertions;
using Moq;
using Moq.Protected;
using Paymentsense.Coding.Challenge.Api.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class HttpServiceTests
    {
        Mock<INetworkCache> cache = new Mock<INetworkCache>();
        Mock<IHttpClientFactory> clientFactory;

        [Fact]
        public async Task GetAsync_ReturnsNewDataIfNothingInCache()
        {
            string content = "some json";
            MockHttpClient(HttpStatusCode.OK, content);
            var service = new HttpService(clientFactory.Object, cache.Object);

            cache.Setup(_ => _.GetString(It.IsAny<string>(), It.IsAny<TimeSpan>()))
                 .Returns(new CacheResult { Exists = false })
                 .Verifiable();
            cache.Setup(_ => _.Save(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var result = await service.GetAsync("https://some.url");

            result.Should().Be(content);
            cache.VerifyAll();
        }

        [Fact]
        public async Task GetAsync_ReturnsCachedDataIfItExists()
        {
            string content = "some json";
            MockHttpClient(HttpStatusCode.OK, content);
            var service = new HttpService(clientFactory.Object, cache.Object);

            cache.Setup(_ => _.GetString(It.IsAny<string>(), It.IsAny<TimeSpan>()))
                 .Returns(new CacheResult { Exists = true, Expired = false, Result = content });

            var result = await service.GetAsync("https://some.url");

            result.Should().Be(content);
            cache.Verify(_ => _.Save(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task GetAsync_ReturnsDataAndUpdatesExpiredCache()
        {
            string content = "some json";
            MockHttpClient(HttpStatusCode.OK, content);
            var service = new HttpService(clientFactory.Object, cache.Object);

            cache.Setup(_ => _.GetString(It.IsAny<string>(), It.IsAny<TimeSpan>()))
                 .Returns(new CacheResult { Exists = true, Expired = true })
                 .Verifiable();
            cache.Setup(_ => _.Save(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var result = await service.GetAsync("https://some.url");

            result.Should().Be(content);
            cache.VerifyAll();
        }

        [Fact]
        public async Task GetAsync_ReturnsNoDataIfNoCacheAndRequestFails()
        {
            MockHttpClient(HttpStatusCode.BadRequest, string.Empty);
            var service = new HttpService(clientFactory.Object, cache.Object);

            cache.Setup(_ => _.GetString(It.IsAny<string>(), It.IsAny<TimeSpan>()))
                 .Returns(new CacheResult { Exists = false });

            var result = await service.GetAsync("https://some.url");

            result.Should().Be(string.Empty);
            cache.Verify(_ => _.Save(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        private void MockHttpClient(HttpStatusCode code, string content)
        {
            var handler = new Mock<HttpMessageHandler>();
            clientFactory = new Mock<IHttpClientFactory>();

            handler.Protected()
                   .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(
                        new HttpResponseMessage
                        {
                            StatusCode = code,
                            Content = new StringContent(content)
                        });

            var client = new HttpClient(handler.Object);

            clientFactory = new Mock<IHttpClientFactory>();
            clientFactory.Setup(c => c.CreateClient(It.IsAny<string>())).Returns(client);
        }
    }
}
