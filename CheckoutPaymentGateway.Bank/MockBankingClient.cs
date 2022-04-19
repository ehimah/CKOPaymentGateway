using System;
using CheckoutPaymentGateway.Service;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Bank
{
	public class MockBankClient: IBankingClient
	{
        const int VALID_CARD_NUMBER_LENGTH = 16;
        const int VALID_CARD_CVV_LENGTH = 16;
		public MockBankClient()
		{
		}

        public Task<TransactionResponse> ProcessPayment(PaymentRequest paymentRequest)
        {
            // build response
            var response = new TransactionResponse() {
                Id = Guid.NewGuid()
            };

            var validationResult = PaymentRequestIsValid(paymentRequest);

            // validate payment request
            if (!validationResult.Item1)
            {
                // decline the request
                response.Status = TransactionStatus.Declined;

                // decline with humane error message
                response.Comment = validationResult.Item2;
            }
            else
            {
                // payment request is valid
                response.Status = TransactionStatus.Accepted;
            }
            

            // return response
            return Task.FromResult(response);
        }

        private static Tuple<bool, string> PaymentRequestIsValid(PaymentRequest paymentRequest)
        {
            var errors = new List<string>();

            // card length
            if(paymentRequest.CardNumber.Length != VALID_CARD_NUMBER_LENGTH)
            {
                errors.Add("Invalid Card Number Length");
            }

            // cvv length
            if (paymentRequest.CardCVV.Length != VALID_CARD_CVV_LENGTH)
            {
                errors.Add("Invalid Card CVV Length");
            }

            // card holder name present
            if (string.IsNullOrWhiteSpace(paymentRequest.CardHolderFullName))
            {
                errors.Add("The Card Holders Name is Empty");
            }

            // card expiry is in future
            var cardExpiry = DateTime.Parse(paymentRequest.CardExpiryDate);
            if (cardExpiry.CompareTo(DateTime.Now) < 0)
            {
                errors.Add("The Card is Expired");
            }

            // compile errors and report
            if(errors.Count > 0)
            {
                return new Tuple<bool, string>(false, string.Join("\n", errors));
            }

            // return a success response
            return new Tuple<bool, string>(true, string.Join("\n", string.Empty));
        }
    }
}

