using System;
using System.Threading.Tasks;
using CheckoutPaymentGateway.Bank;
using CheckoutPaymentGateway.Service.Models;
using Xunit;

namespace CheckoutPaymentGateway.Tests.API.Unit
{
	public class BankClientTest
	{
		public BankClientTest()
		{
		}


		[Fact]
		public async Task ProcessPayment_WhenCalledWithValidPaymentRequest_ShouldReturnValidResult()
        {
			// Arrange
			MockBankClient bankClient = new();
			var paymentRequest = new PaymentRequest {
                Id = Guid.NewGuid(),
				CardNumber = "1234123412341234",
				CardHolderFullName = "EHIMAH OBUSE",
				CardExpiryDate = "04/25",
				CardCVV = "NGN",
				Amount = 12.34m,
				Currency = "GBP",
			};

			// Act
			var response = await bankClient.ProcessPayment(paymentRequest);

			//Assert
			Assert.Equal(TransactionStatus.Accepted, response.Status);
			Assert.True(string.IsNullOrWhiteSpace(response.Comment));

		}

		[Fact]
		public async Task ProcessPayment_WhenCalledWithInValidPaymentRequest_ShouldReturnDeclinedResult()
        {
			// Arrange
			MockBankClient bankClient = new();
			var paymentRequest = new PaymentRequest {
                Id = Guid.NewGuid(),
				CardNumber = "1234123412341234",
				// invalid cardholder name
				CardHolderFullName = string.Empty,
				CardExpiryDate = "04/25",
				// wrong CVV length
				CardCVV = "90",
				Amount = 12.34m,
				Currency = "GBP",
			};

			// Act
			var response = await bankClient.ProcessPayment(paymentRequest);

			//Assert
			Assert.Equal(TransactionStatus.Declined, response.Status);
			Assert.Contains("Invalid Card CVV Length", response.Comment);
			Assert.Contains("The Card Holders Name is Empty", response.Comment);


		}
	}
}

