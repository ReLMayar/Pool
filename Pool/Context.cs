using Microsoft.EntityFrameworkCore;
using Pool.Models;

namespace Pool
{
    public class Context : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Bowl> Bowls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder db)
        {
            db.UseMySQL("server=localhost;UserId=root;Password=Gsnuji48i4fy4D89fp0jc3f74ound3kw _;database=pool;");
        }
    }
}
