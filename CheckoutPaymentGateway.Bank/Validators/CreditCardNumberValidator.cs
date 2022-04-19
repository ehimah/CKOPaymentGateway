using System;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Bank.Validators
{
	public class CreditCardNumberValidator : IPaymentRequestValidator
	{
        const int VALID_CARD_NUMBER_LENGTH = 16;

        public string ErrorMessage => "Invalid Card Number Length";

        public bool IsValid(PaymentRequest paymentRequest)
        {
            return paymentRequest.CardNumber.Length == VALID_CARD_NUMBER_LENGTH;
        }
    }
}

