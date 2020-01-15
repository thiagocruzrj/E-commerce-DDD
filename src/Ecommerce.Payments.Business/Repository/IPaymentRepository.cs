using Ecommerce.Core.Data;
using Ecommerce.Payments.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Payments.Business.Repository
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void Add(Payment payment);
        void AddTransaction(Transaction transaction);
    }
}
