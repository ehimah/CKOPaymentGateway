using System;
using CheckoutPaymentGateway.Service.Entities;

namespace CheckoutpaymentGateway.Service
{
	public interface IPaymentRepository
	{
		/// <summary>
        /// Fetches a payment
        /// </summary>
        /// <param name="id">the id of the payment to be retrieved</param>
        /// <returns>The payment item</returns>
		Task<Payment> GetPayment(Guid id);

		/// <summary>
        /// Saves a payment
        /// </summary>
        /// <param name="payment">the payment to be saved</param>
        /// <returns>The id of the saved payment</returns>
		Task<Guid> Save(Payment payment);
		
	}
}

