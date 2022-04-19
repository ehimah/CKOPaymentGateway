using System;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Bank.Validators
{
	public class CreditCardCvvValidator: IPaymentRequestValidator
	{
		const int VALID_CARD_CVV_LENGTH = 3;

        public string ErrorMessage => "Invalid Card CVV Length";

        public bool IsValid(PaymentRequest paymentRequest)
        {
            return paymentRequest.CardCVV.Length == VALID_CARD_CVV_LENGTH;
        }
    }
}

