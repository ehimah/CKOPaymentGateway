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
        var payment = PaymentBuilder.PartialFromRequest(request);

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
}

