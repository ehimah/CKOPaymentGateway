using System;
using AutoMapper;
using CheckoutPaymentGateway.Service.Entities;
using CheckoutPaymentGateway.Service.Models;

namespace CheckOutPaymentGateway.API.Dto
{
	public class PaymentPaymentResponseProfile: Profile
	{
		public PaymentPaymentResponseProfile()
		{
			CreateMap<Payment, PaymentResponse>().ReverseMap();
		}
	}
}

