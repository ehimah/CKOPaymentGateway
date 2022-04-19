using System;
using System.Linq;
using CheckoutPaymentGateway.Bank.Validators;
using CheckoutPaymentGateway.Service;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Bank
{
    class ValidationResult
    {
        public bool IsValid;
        public string ErrorMessages;
    }

    public class MockBankClient: IBankingClient
	{
        public Task<TransactionResponse> ProcessPayment(PaymentRequest paymentRequest)
        {
            // build response
            var response = new TransactionResponse() {
                Id = Guid.NewGuid()
            };

            var validationResult = PaymentRequestIsValid(paymentRequest);

            // validate payment request
            if (!validationResult.IsValid)
            {
                // decline the request
                response.Status = TransactionStatus.Declined;

                // decline with humane error message
                response.Comment = validationResult.ErrorMessages;
            }
            else
            {
                // payment request is valid
                response.Status = TransactionStatus.Accepted;
            }
            

            // return response
            return Task.FromResult(response);
        }

        /// <summary>
        /// Validates a Payment Request
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        private static ValidationResult PaymentRequestIsValid(PaymentRequest paymentRequest)
        {

            // hacky, better to expose a routine to make this list extensible
            List<IPaymentRequestValidator> validators = new()
            {
                    new CreditCardCvvValidator(),
                    new CreditCardExpiryValidator(),
                    new CreditCardNameValidator(),
                    new CreditCardNumberValidator(),
            };

            // compile errors and report
            var errors = validators.Where(validator => !validator.IsValid(paymentRequest))
                .Select(validator => validator.ErrorMessage).ToArray();

            
            var result =  new ValidationResult {
                IsValid = errors.Length == 0,
                ErrorMessages = string.Join("\n", errors)
            };

            return result;
        }

        
    }
}

