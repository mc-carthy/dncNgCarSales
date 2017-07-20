using System.Threading.Tasks;

namespace dncNgCarSales.Persistence
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}