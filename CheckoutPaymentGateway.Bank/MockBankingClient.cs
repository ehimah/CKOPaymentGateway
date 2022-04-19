﻿using System;
using CheckoutPaymentGateway.Service;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Bank
{
	public class MockBankClient: IBankingClient
	{
		public MockBankClient()
		{
		}

        public Task<TransactionResponse> ProcessPayment(PaymentRequest paymentRequest)
        {
            // build response
            var response = new TransactionResponse() {
                Id = Guid.NewGuid()
            };

            // validate payment request
            if (!this.PaymentRequestIsValid(paymentRequest))
            {
                // decline the request
                // TODO: decline with humane error message
                response.Status = TransactionStatus.Declined;
            }
            response.Status = TransactionStatus.Accepted;

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

