using System;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Bank.Validators
{
	public class CreditCardNameValidator : IPaymentRequestValidator
	{
        public string ErrorMessage => "The Card Holders Name is Empty";

        public bool IsValid(PaymentRequest paymentRequest)
        {
            return !string.IsNullOrWhiteSpace(paymentRequest.CardHolderFullName);
        }
    }
}

