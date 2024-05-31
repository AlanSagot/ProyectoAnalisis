using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class LeahDBContext : DbContext
    {
        public LeahDBContext() { }

        public LeahDBContext(DbContextOptions<LeahDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer();
        }

    }
}
