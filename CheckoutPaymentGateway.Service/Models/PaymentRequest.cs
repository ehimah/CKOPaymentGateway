using System;
namespace CheckoutPaymentGateway.Service.Models
{
	public class PaymentRequest
	{
		/// <summary>
		/// Request Id used for deduplicating requests
		/// </summary>
		public Guid Id { get; set; }
		/// <summary>
        /// The Full name of the card holder
        /// </summary>
		public string CardHolderFullName { get; set; }
		/// <summary>
        /// The credit card Number
        /// </summary>
		public string CardNumber { get; set; }
		/// <summary>
        /// The credit card expiry month and year (MM/yy). e.g. 04/25
        /// </summary>
		public string CardExpiryDate { get; set; }
		/// <summary>
        /// The creit card CVV
        /// </summary>
		public string CardCVV { get; set; }
		/// <summary>
        /// The payment transaction amount
        /// </summary>
        public decimal Amount { get; set; }
		/// <summary>
        /// The currency of the payment transaction
        /// </summary>
        public string Currency { get; set; }
    }
}

