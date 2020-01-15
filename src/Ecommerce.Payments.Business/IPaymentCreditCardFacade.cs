using Ecommerce.Payments.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Payments.Business
{
    public interface IPaymentCreditCardFacade
    {
        Transaction MakePayment(Order order, Payment payment);
    }
}
