using System;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Service.Entities
{
    public class Payment
	{
        /// <summary>
        /// The unique Id of this payment
        /// </summary>
		public Guid Id { get; set; }

        /// <summary>
        /// The external transaction reference of the associated bank transaction
        /// </summary>
		public Guid ExternalReference { get; set; }

        /// <summary>
        /// The error details returned for a non succesfull transaction
        /// </summary>
        public string ExternalComment { get; set; }

        /// <summary>
        /// The status of the payment transaction
        /// </summary>
        public TransactionStatus Status { get; set; }

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

