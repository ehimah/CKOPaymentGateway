using System;
using AutoMapper;
using CheckoutPaymentGateway.Service.Models;

namespace CheckOutPaymentGateway.API.Dto
{
	public class PaymentResponseProfile: Profile
	{
		public PaymentResponseProfile()
		{
			CreateMap<PaymentResponseDto, PaymentResponse>().ReverseMap();
		}
	}
}

