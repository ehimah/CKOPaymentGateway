using System;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Bank.Validators
{
	public interface IPaymentRequestValidator
	{
		bool IsValid(PaymentRequest paymentRequest);
        string ErrorMessage { get; }
    }
}

