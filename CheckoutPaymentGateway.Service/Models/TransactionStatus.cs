using System;
using System.Runtime.Serialization;

namespace CheckoutPaymentGateway.Service.Models
{
    /// <summary>
    /// The status of payment transactions
    /// </summary>
    public enum TransactionStatus
	{
        [EnumMember(Value = "Accepted")] Accepted,
        [EnumMember(Value = "Declined")] Declined
	}
}

