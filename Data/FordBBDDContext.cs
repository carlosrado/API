using Microsoft.EntityFrameworkCore;

namespace API.Data{
    public class FordBBDDContext : DbContext{
        public FordBBDDContext(DbContextOptions<FordBBDDContext> options): base(options)
        {

        }    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FixQtyClass>()
            .ToTable("FixQties", f => f.IsTemporal(
                f => {
                    f.HasPeriodEnd("PeriodEnd");
                    f.HasPeriodStart("PeriodStart");
                    f.UseHistoryTable("FixQtiesHistory");
                }
            ));
              
        }
        public DbSet<FixQtyClass> FixQties => Set<FixQtyClass>();
        public DbSet<UserClass> Users => Set<UserClass>();

    }
}