using CheckoutpaymentGateway.Service;
using CheckoutPaymentGateway.Service.Entities;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Service;
public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository repository;
    private readonly IBankingClient bankingClient;

    public PaymentService(IPaymentRepository repository,
        IBankingClient bankingClient)
    {
        this.repository = repository;
        this.bankingClient = bankingClient;
    }

    public async Task<Payment> GetPaymentInfo(Guid paymentId)
    {
        var payment = await repository.GetPayment(paymentId);
        return payment;
    }

    public async Task<Payment> ProcessPayment(PaymentRequest request)
    {
        // save this transaction as pending
        // build a partial payment Item based of the request data
        var payment = new Payment
        {
            Id = request.Id,
            Amount = request.Amount,
            CardExpiryDate = request.CardExpiryDate,
            CardHolderFullName = request.CardHolderFullName,
            CardNumber = MaskCreditCardNumber(request.CardNumber),
            Currency = request.Currency,
        };

        // set the transaction as pending
        payment.Status = TransactionStatus.Pending;

        await this.repository.Save(payment);

        // call acquiring bank to process transaction
        var transactionResponse = await this.bankingClient.ProcessPayment(request);

        // update the payent item to reflect the new status and comments from the acquiring bank
        payment.ExternalReference = transactionResponse.Id;
        payment.Status = transactionResponse.Status;
        payment.ExternalComment = transactionResponse.Comment;

        // update the payment information to storage
        await this.repository.Save(payment);

        return payment;
    }

    /// <summary>
    /// Mask the credit card number by converting all characters to the specified mask char but last 4
    /// </summary>
    /// <param name="creditCardNumber">The credit card number to mask</param>
    /// <param name="maskChar">The mask char to use. Default is *</param>
    /// <returns></returns>
    private static string MaskCreditCardNumber(string creditCardNumber, char maskChar = '*')
    {
        var cardMask = creditCardNumber[^4..].PadLeft(creditCardNumber.Length, maskChar);

        return cardMask;
    }
}

