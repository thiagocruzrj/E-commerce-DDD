using Ecommerce.Core.Data;
using Ecommerce.Payments.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Payments.Business.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(Payment payment)
        {
            throw new NotImplementedException();
        }

        public void AddTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
