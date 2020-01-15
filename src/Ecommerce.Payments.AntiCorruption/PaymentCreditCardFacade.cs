using Ecommerce.Payments.AntiCorruption.Config;
using Ecommerce.Payments.AntiCorruption.Gateway;
using Ecommerce.Payments.Business;
using Ecommerce.Payments.Business.Entities;
using Ecommerce.Payments.Business.Enums;

namespace Ecommerce.Payments.AntiCorruption
{
    public class PaymentCreditCardFacade : IPaymentCreditCardFacade
    {
        private readonly IPayPalGateway _payPalGateway;
        private readonly IConfigurationManager _configurationManger;

        public PaymentCreditCardFacade(IPayPalGateway payPalGateway, IConfigurationManager configurationManger)
        {
            _payPalGateway = payPalGateway;
            _configurationManger = configurationManger;
        }

        public Transaction MakePayment(Order order, Payment payment)
        {
            var apiKey = _configurationManger.GetValue("apiKey");
            var encriptionKey = _configurationManger.GetValue("encriptionKey");

            var serviceKey = _payPalGateway.GetPayPalServiceKey(apiKey, encriptionKey);
            var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, payment.CardNumber);

            var paymentResult = _payPalGateway.CommitTransaction(cardHashKey, order.Id.ToString(), payment.Value);

            // TODO: Payment gateway must to return the transaction object
            var transaction = new Transaction
            {
                OrderId = order.Id,
                Total = order.Value,
                PaymentId = payment.Id
            };

            if (paymentResult)
            {
                transaction.StatusTransaction = StatusTransaction.Paid;
                return transaction;
            }

            transaction.StatusTransaction = StatusTransaction.Rejected;
            return transaction;
        }
    }
}
