using System;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Service
{
	public interface IBankingClient
	{
		Task<PaymentResponse> ProcessPayment(PaymentRequest paymentRequest);
	}
}

