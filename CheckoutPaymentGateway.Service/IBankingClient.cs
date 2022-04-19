using System;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Service
{
	public interface IBankingClient
	{
		Task<TransactionResponse> ProcessPayment(PaymentRequest paymentRequest);
	}
}

