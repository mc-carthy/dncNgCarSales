using System.Threading.Tasks;

namespace dncNgCarSales.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SkeletonDbContext context;
        public UnitOfWork(SkeletonDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}