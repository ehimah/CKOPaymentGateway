using System;
namespace CheckoutPaymentGateway.Service.Models
{
	public class PaymentResponse
	{
        public Guid Id { get; set; }
        public Guid ExternalReference { get; set; }
        public TransactionStatus Status { get; set; }
        public string ExternalComment { get; set; }

        public string CardHolderFullName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiryDate { get; set; }
        public double Amount { get; set; }
    }
}

