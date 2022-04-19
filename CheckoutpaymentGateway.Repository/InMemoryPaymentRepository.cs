using System;
using CheckoutpaymentGateway.Service;
using CheckoutPaymentGateway.Service.Entities;

namespace CheckoutpaymentGateway.Repository
{
	public class InMemoryPaymentRepository : IPaymentRepository
	{
        private readonly Dictionary<Guid, Payment> paymentStore;
		public InMemoryPaymentRepository ()
		{
            this.paymentStore = new Dictionary<Guid, Payment>();
		}

        public Task<Payment>? GetPayment(Guid id)
        {
            // if payment exist, return payment item from store
            if (paymentStore.ContainsKey(id))
            {
                var payment = paymentStore[id];
                return Task.FromResult(payment);
            }

            // payment not found, return null
            return null;
        }

        public Task<Payment> Save(Payment payment)
        {
            // save the payment item to storage and return
            paymentStore.Add(payment.Id, payment);
            return Task.FromResult(payment);
        }
    }
}

