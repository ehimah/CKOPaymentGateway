using System;
using System.Globalization;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Bank.Validators
{
	public class CreditCardExpiryValidator : IPaymentRequestValidator
	{
        public string ErrorMessage => "The Card is Expired";

        public bool IsValid(PaymentRequest paymentRequest)
        {
            // card expiry is in future
            var cultureInfo = new CultureInfo(CultureInfo.CurrentCulture.LCID);

            // avoid counting dates in the 1900s
            cultureInfo.Calendar.TwoDigitYearMax = 2099; 
            var cardExpiry = DateTime.ParseExact(paymentRequest.CardExpiryDate, "MM/yy", cultureInfo);

            // if card expirty date is not earlier than current date time
            if (cardExpiry.CompareTo(DateTime.Now) > 0)
            {
                return true;
            }

            return false;
        }
    }
}

