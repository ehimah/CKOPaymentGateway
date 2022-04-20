using System;
using System.ComponentModel.DataAnnotations;

namespace CheckoutPaymentGateway.Service.Models
{
	public class PaymentRequest
	{
        /// <summary>
        /// Request Id used for deduplicating requests
        /// </summary>
        [Required]
        public Guid Id { get; set; }

		/// <summary>
        /// The Full name of the card holder
        /// </summary>
        [Required]
		public string CardHolderFullName { get; set; }

        /// <summary>
        /// The credit card Number
        /// </summary>
        ///
        [Required]
        public string CardNumber { get; set; }

        /// <summary>
        /// The credit card expiry month and year (MM/yy). e.g. 04/25
        /// </summary>
        ///
        [Required]
        public string CardExpiryDate { get; set; }

        /// <summary>
        /// The creit card CVV
        /// </summary>
        ///
        [Required]
        public string CardCVV { get; set; }

        /// <summary>
        /// The payment transaction amount
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// The currency of the payment transaction
        /// </summary>
        [Required]
        public string Currency { get; set; }
    }
}

