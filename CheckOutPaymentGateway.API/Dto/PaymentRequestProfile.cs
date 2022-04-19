using System;
using AutoMapper;
using CheckoutPaymentGateway.Service.Models;

namespace CheckOutPaymentGateway.API.Dto
{
	public class PaymentRequestProfile: Profile
	{
		public PaymentRequestProfile()
		{
			CreateMap<PaymentRequestDto, PaymentRequest>().ReverseMap();
		}
	}
}

