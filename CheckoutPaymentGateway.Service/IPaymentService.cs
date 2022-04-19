using System;
using CheckoutPaymentGateway.Service.Entities;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Service
{
	public interface IPaymentService
	{
		/// <summary>
		/// Fetch and return a payment record
		/// </summary>
		/// <param name="paymentId">Id of the payment</param>
		/// <returns>The payment record</returns>
		Task<Payment> GetPaymentInfo(Guid paymentId);

		/// <summary>
		/// Processes a payment request
		/// </summary>
		/// <param name="request">The reqyuest payload</param>
		/// <returns>The response from the payment processor</returns>
		Task<Payment> ProcessPayment(PaymentRequest request );

	}
}

