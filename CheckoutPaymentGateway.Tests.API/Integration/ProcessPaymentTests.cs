using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Bogus;
using CheckoutPaymentGateway.Service.Models;
using CheckoutPaymentGateway.Tests.API.Helpers;
using Xunit;

namespace CheckoutPaymentGateway.Tests.API;

public class ProcessPaymentTests: IClassFixture<CustomWebApplicationFactory<CheckOutPaymentGateway.API.Startup>>
{
    const string REST_API_URL = "api/payment";
    private readonly HttpClient httpClient;

    public ProcessPaymentTests(CustomWebApplicationFactory<CheckOutPaymentGateway.API.Startup> factory)
    {
        httpClient = factory.CreateClient();
        var accessToken = FakeTokenProvider.GetAccessToken();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    private static PaymentRequest GetMockPayload(Guid id)
    {
        var faker = new Faker();
        var payload = new PaymentRequest
        {
            Id = id,
            CardNumber = "1234123412341234",
            CardHolderFullName = faker.Name.FullName(),
            CardExpiryDate = "04/25",
            CardCVV = faker.Finance.CreditCardCvv(),
            Amount = faker.Finance.Amount(),
            Currency = faker.Finance.Currency().Code,
        };
        return payload;
    }

    [Fact]
    public async void ProcessPayment_WhenCalledWithValidParameters_ReturnsCreatedResult()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Post, REST_API_URL)
        {
            Content = new StringContent(JsonSerializer.Serialize(GetMockPayload(Guid.NewGuid())), Encoding.UTF8, "application/json")
        };

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async void ProcessPayment_WhenCalledWithDuplicateRequestParameters_ReturnsConflictResult()
    {
        // Scene 1
        // Arrange
        var requestId = Guid.NewGuid();

        var request1 = new HttpRequestMessage(HttpMethod.Post, REST_API_URL)
        {
            Content = new StringContent(JsonSerializer.Serialize(GetMockPayload(requestId)), Encoding.UTF8, "application/json")
        };

        // Act
        var response = await httpClient.SendAsync(request1);

        // Assert that 1st call results in created result
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);


        // Scene 2
        var request2 = new HttpRequestMessage(HttpMethod.Post, REST_API_URL)
        {
            Content = new StringContent(JsonSerializer.Serialize(GetMockPayload(requestId)), Encoding.UTF8, "application/json")
        };
        // Call endpoint seconfd time with same request id
        var response2 = await httpClient.SendAsync(request2);

        // Assert that the result is a conflict result
        Assert.Equal(HttpStatusCode.Conflict, response2.StatusCode);
    }
}
