using Ecommerce.Core.DomainObjects.DTO;
using Ecommerce.Payments.Business.Entities;
using System.Threading.Tasks;

namespace Ecommerce.Payments.Business.Services
{
    public interface IPaymentService
    {
        Task<Transaction> MakeOrderPayment(PaymentOrder paymentOrder);
    }
}
