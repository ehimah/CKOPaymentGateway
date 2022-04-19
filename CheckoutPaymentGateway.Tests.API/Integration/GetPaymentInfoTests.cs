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

public class GetPaymentInfoTests : IClassFixture<WebApplicationFactory<CheckOutPaymentGateway.API.Startup>>
{
    const string REST_API_URL = "api/payment";
    private readonly HttpClient httpClient;

    public GetPaymentInfoTests(WebApplicationFactory<CheckOutPaymentGateway.API.Startup> factory)
    {
        httpClient = factory.CreateClient();
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
