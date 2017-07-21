using System.Threading.Tasks;

namespace dncNgCarSales.Core
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}