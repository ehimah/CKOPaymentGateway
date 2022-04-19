using System;
namespace CheckoutPaymentGateway.Service.Models
{
	public class PaymentRequest
	{
		public Guid Id { get; set; }
		public string CardHolderFullName { get; set; }
		public string CardNumber { get; set; }
		public string CardExpiryDate { get; set; }
		public string CardCVV { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}

