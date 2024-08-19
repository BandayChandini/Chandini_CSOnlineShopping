using Microsoft.EntityFrameworkCore;
namespace OnlineShoppingAPI.Entities
{
    public class OnlineShoppingContext:DbContext
    {
        private IConfiguration configuration;

        public OnlineShoppingContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Category>Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply unique constraint on Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
           

            // Additional configurations can be done here
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your database provider and connection string here
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("OnlineShoppingConnection"));
        }
    }
}
 