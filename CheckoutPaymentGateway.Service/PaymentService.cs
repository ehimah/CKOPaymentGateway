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

    Task<Payment> IPaymentService.GetPaymentInfo(Guid paymentId)
    {
        throw new NotImplementedException();
    }

    Task<PaymentResponse> IPaymentService.ProcessPayment(PaymentRequest request)
    {
        throw new NotImplementedException();
    }
}

