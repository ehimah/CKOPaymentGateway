using System;
using CheckoutPaymentGateway.Service.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CheckOutPaymentGateway.API.Dto
{
	public class PaymentResponseDto
	{
		public Guid Id { get; set; }
		public Guid ExternalReference { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public TransactionStatus Status { get; set; }

		public string CardHolderFullName { get; set; }
		public string CardNumber { get; set; }
		public string CardExpiryDate { get; set; }
		public double Amount { get; set; }
	}
}

