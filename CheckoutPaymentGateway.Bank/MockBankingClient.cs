using System;
using CheckoutPaymentGateway.Service;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Bank
{
	public class MockBankClient: IBankingClient
	{
		public MockBankClient()
		{
		}

        public Task<PaymentResponse> ProcessPayment(PaymentRequest paymentRequest)
        {
            // build response
            var response = new PaymentResponse() { ExternalReference = Guid.NewGuid() };

            // validate payment request
            if (!this.PaymentRequestIsValid(paymentRequest))
            {
                // decline the request
                // TODO: decline with humane error message
                response.Status = TransactionStatus.Declined;
            }

            // return response
            return Task.FromResult(response);
        }

        private bool PaymentRequestIsValid(PaymentRequest paymentRequest)
        {
            // TODO: validate the following
            // card length
            // cvv length
            // card holder name present
            // card expiry is in future
            return false;
        }
    }
}

