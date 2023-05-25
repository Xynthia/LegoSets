using LegoSets.Models;
using Microsoft.EntityFrameworkCore;

namespace LegoSets.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<LegoSet> LegoSet => Set<LegoSet>();
    }
}
