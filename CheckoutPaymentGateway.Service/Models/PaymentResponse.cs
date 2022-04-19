using System;
namespace CheckoutPaymentGateway.Service.Models
{
	public class PaymentResponse
	{
        public Guid Id { get; set; }
        public Guid ExternalReference { get; set; }
        public TransactionStatus Status { get; set; }
        public string ExternalComment { get; set; }
    }
}

