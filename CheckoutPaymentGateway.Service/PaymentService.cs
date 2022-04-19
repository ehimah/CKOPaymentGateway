using CheckoutpaymentGateway.Service;
using CheckoutPaymentGateway.Service.Entities;
using CheckoutPaymentGateway.Service.Models;

namespace CheckoutPaymentGateway.Service;
public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository repository;
    private readonly IBankingClient bankingClient;

    public PaymentService(IPaymentRepository repository, IBankingClient bankingClient)
    {
        this.repository = repository;
        this.bankingClient = bankingClient;
    }

    public async Task<Payment> GetPaymentInfo(Guid paymentId)
    {
        var payment = await repository.GetPayment(paymentId);
        return payment;
    }

    public async Task<PaymentResponse> ProcessPayment(PaymentRequest request)
    {
        // call acquiring bank to process transaction
        var paymnentResponse = await this.bankingClient.ProcessPayment(request);

        // save the payment information to storage
        var payment = await this.repository.Save(PaymentBuilder.FromRequestResponse(request, paymnentResponse));

        paymnentResponse.Id = payment.Id;

        return paymnentResponse;
    }
}

