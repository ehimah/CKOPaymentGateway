using System;
using CheckoutpaymentGateway.Service;
using CheckoutPaymentGateway.Service.Entities;

namespace CheckoutpaymentGateway.Repository
{
	public class PaymentRepository: IPaymentRepository
	{
		public PaymentRepository()
		{
		}

        Task<Payment> IPaymentRepository.GetPayment(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<Guid> IPaymentRepository.Save(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}

