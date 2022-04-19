using System;
namespace CheckoutPaymentGateway.Service.Models
{
	public class TransactionResponse
	{
		public TransactionResponse()
		{
		}

		public Guid Id { get; set; }
		public TransactionStatus Status { get; set; }
        public string Comment { get; set; }
    }
}

