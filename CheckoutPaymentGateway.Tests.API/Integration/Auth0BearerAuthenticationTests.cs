using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CheckoutPaymentGateway.Service.Models;
using CheckoutPaymentGateway.Tests.API.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CheckoutPaymentGateway.Tests.API.Integration
{
    public class Auth0BearerAuthenticationTests : IClassFixture<WebApplicationFactory<CheckOutPaymentGateway.API.Startup>>, IAsyncLifetime
    {
        const string REST_API_URL = "api/payment";
        private readonly HttpClient httpClient;
        private readonly Guid TestPaymentId;



        public Auth0BearerAuthenticationTests(WebApplicationFactory<CheckOutPaymentGateway.API.Startup> factory)
        {
            httpClient = factory.CreateClient();

        }

        public async Task InitializeAsync()
        {
            var accessToken = await AuthTokenProvider.GetAccessToken();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        private PaymentRequest GetSamplePayload ()
        {
            return new PaymentRequest
            {
                Id = TestPaymentId,
                CardNumber = "1234123412341234",
                CardHolderFullName = "EHIMAH OBUSE",
                CardExpiryDate = "04/25",
                CardCVV = "NGN",
                Amount = 12.34m,
                Currency = "GBP",
            };
        }

        [Fact]
        public async void ControllerAction_WhenCalledWithValidBearerToken_GivesSuccessResult()
        {
            // Arrange
       
            var request = new HttpRequestMessage(HttpMethod.Post, REST_API_URL)
            {
                Content = new StringContent(JsonSerializer.Serialize(GetSamplePayload()), Encoding.UTF8, "application/json")
            };

            // Act
            var response = await httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }


        [Fact]
        public async void ControllerAction_WhenCalledWithNoBearerToken_GivesUnAuthorizedResult()
        {
            // Arrange
            
            var request = new HttpRequestMessage(HttpMethod.Post, REST_API_URL)
            {
                Content = new StringContent(JsonSerializer.Serialize(GetSamplePayload()), Encoding.UTF8, "application/json")
            };
            //clear auth headers
            httpClient.DefaultRequestHeaders.Authorization = null;

            // Act
            var response = await httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}

