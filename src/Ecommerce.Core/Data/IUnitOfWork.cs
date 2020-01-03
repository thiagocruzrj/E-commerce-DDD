using System.Threading.Tasks;

namespace Ecommerce.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
