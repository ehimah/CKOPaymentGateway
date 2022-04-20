﻿using System;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Service.Entities
{
	public class Payment
	{
		public Guid Id { get; set; }
		public Guid ExternalReference { get; set; }
        public string ExternalComment { get; set; }
        public TransactionStatus Status { get; set; }

		public string CardHolderFullName { get; set; }
		public string CardNumber { get; set; }
		public string CardExpiryDate { get; set; }
		public decimal Amount { get; set; }
        public string Currency { get; set; }
	}

	public class PaymentBuilder
	{
		public static Payment PartialFromRequest(PaymentRequest request)
        {
			var cardMask = request.CardNumber.Substring(request.CardNumber.Length - 4).PadLeft(request.CardNumber.Length, '*');
			var payment = new Payment
			{
				Id = request.Id,
				Amount = request.Amount,
				CardExpiryDate = request.CardExpiryDate,
				CardHolderFullName = request.CardHolderFullName,
				CardNumber = cardMask,
				Currency = request.Currency,
			};

			return payment;
		}
		public static Payment FromRequestResponse(PaymentRequest request, PaymentResponse response)
        {
			var cardMask = request.CardNumber.Substring(request.CardNumber.Length - 4).PadLeft(request.CardNumber.Length, '*');
			var payment = new Payment
			{
				Id= request.Id,
				Amount = request.Amount,
				CardExpiryDate = request.CardExpiryDate,
				CardHolderFullName = request.CardHolderFullName,
				CardNumber = cardMask,
				Currency = request.Currency,
				Status = response.Status,
				ExternalReference = response.ExternalReference

			};

			return payment;
        }
	}


}

