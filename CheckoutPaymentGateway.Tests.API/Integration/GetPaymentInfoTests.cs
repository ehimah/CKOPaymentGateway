using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CheckoutPaymentGateway.Service.Models;
using CheckoutPaymentGateway.Tests.API.Helpers;
using Xunit;

namespace CheckoutPaymentGateway.Tests.API;

public class GetPaymentInfoTests : IClassFixture<CustomWebApplicationFactory<CheckOutPaymentGateway.API.Startup>>, IAsyncLifetime
{
    const string REST_API_URL = "api/payment";
    private const string CARD_MASK_REGEX = @"\*{12}\d{4}";
    private readonly HttpClient httpClient;
    private readonly Guid TestPaymentId;
    


    public GetPaymentInfoTests(CustomWebApplicationFactory<CheckOutPaymentGateway.API.Startup> factory)
    {
        httpClient = factory.CreateClient();

        var accessToken = FakeTokenProvider.GetAccessToken();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


        //setup code
        TestPaymentId = Guid.NewGuid();

    }

    public async Task InitializeAsync()
    {
        await PopulateTestPayments();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public async void GetPayment_WhenCalledWithValidId_ReturnsPaymentItem()
    {
        // Act
        var response = await httpClient.GetAsync($"{REST_API_URL}/{TestPaymentId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();

        var item = JsonSerializer.Deserialize<PaymentResponse>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        Assert.NotNull(item);
        Assert.Matches(CARD_MASK_REGEX, item.CardNumber);
    }

    

    private async Task PopulateTestPayments()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, REST_API_URL);

        var payload = new PaymentRequest
        {
            Id = TestPaymentId,
            CardNumber = "1234123412341234",
            CardHolderFullName = "EHIMAH OBUSE",
            CardExpiryDate = "04/25",
            CardCVV = "NGN",
            Amount = 12.34m,
            Currency = "GBP",
        };

        request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        // Act
        await httpClient.SendAsync(request);
    }

    
}
