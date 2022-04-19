using System;
namespace CheckOutPaymentGateway.API.Dto
{
	public class PaymentRequestDto
	{
		public PaymentRequestDto()
		{
		
		}
		/// <summary>
        /// Request Id used for deduplicating requests
        /// </summary>
        public Guid Id { get; set; }
        public string CardHolderFullName { get; set; }
		public string CardNumber { get; set; }
		public string CardExpiryDate { get; set; }
		public string CardCVV { get; set; }
		public double Amount { get; set; }
		public string Currency { get; set; }
	}
}

