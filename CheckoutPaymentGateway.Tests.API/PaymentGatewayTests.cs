using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using CheckoutPaymentGateway.Service.Models;
using CheckOutPaymentGateway.API.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CheckoutPaymentGateway.Tests.API;

public class PaymentGatewayTests: IClassFixture<WebApplicationFactory<CheckOutPaymentGateway.API.Startup>>
{
    const string REST_API_URL = "api/payment";
    private readonly HttpClient httpClient;

    public PaymentGatewayTests(WebApplicationFactory<CheckOutPaymentGateway.API.Startup> factory)
    {
        httpClient = factory.CreateClient();
    }

    [Fact]
    public async void ProcessPayment_WhenCalledWithValidParameters_ReturnsCreatedResult()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Post, REST_API_URL);

        var payload = new PaymentRequest
        {
            Id = Guid.NewGuid(),
            CardNumber = "1234123412341234",
            CardHolderFullName = "EHIMAH OBUSE",
            CardExpiryDate = "04/25",
            CardCVV = "NGN",
            Amount = 12.34,
            Currency = "GBP",
        };

        request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async void ProcessPayment_WhenCalledWithDuplicateRequestParameters_ReturnsConflictResult()
    {
        // Arrange
        var request1 = new HttpRequestMessage(HttpMethod.Post, REST_API_URL);

        var payload = new PaymentRequest
        {
            Id = Guid.NewGuid(),
            CardNumber = "1234123412341234",
            CardHolderFullName = "EHIMAH OBUSE",
            CardExpiryDate = "04/25",
            CardCVV = "NGN",
            Amount = 12.34,
            Currency = "GBP",
        };

        request1.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        // Act
        var response = await httpClient.SendAsync(request1);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        // Request 2
        var request2 = new HttpRequestMessage(HttpMethod.Post, REST_API_URL);
        // update the card detail
        payload.CardNumber = "9876543210987654";
        request2.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        // Call endpoint seconfd time with same request id
        var response2 = await httpClient.SendAsync(request2);

        Assert.Equal(HttpStatusCode.Created, response2.StatusCode);
    }


    [Fact]
    public async void GetPayment_WhenCalledWithValidId_ReturnsPaymentItem()
    {
        // Act
        var paymentId = Guid.NewGuid();
        var response = await httpClient.GetAsync($"{REST_API_URL}/{paymentId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();

        var item = JsonSerializer.Deserialize<object>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        Assert.NotNull(item);
    }
}
