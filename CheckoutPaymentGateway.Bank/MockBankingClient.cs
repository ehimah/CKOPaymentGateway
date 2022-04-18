using System;
using CheckoutPaymentGateway.Service;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Bank
{
	public class MockBankClient: IBankingClient
	{
		public MockBankClient()
		{
		}

        Task<PaymentResponse> IBankingClient.ProcessPayment(PaymentRequest paymentRequest)
        {
            throw new NotImplementedException();
        }
    }
}

