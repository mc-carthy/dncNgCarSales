using Microsoft.EntityFrameworkCore;

namespace dncNgCarSales.Persistence
{
    public class SkeletonDbContext : DbContext
    {
        public SkeletonDbContext(DbContextOptions<SkeletonDbContext> options)
            : base(options)
        {
        }
    }
}