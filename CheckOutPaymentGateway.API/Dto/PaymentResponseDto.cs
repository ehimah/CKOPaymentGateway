using System;
using CheckoutPaymentGateway.Service.Models;

namespace CheckOutPaymentGateway.API.Dto
{
	public class PaymentResponseDto
	{
		public PaymentResponseDto()
		{

		}
		public Guid Id { get; set; }
		public Guid ExternalReference { get; set; }
		public TransactionStatus Status { get; set; }
	}
}

