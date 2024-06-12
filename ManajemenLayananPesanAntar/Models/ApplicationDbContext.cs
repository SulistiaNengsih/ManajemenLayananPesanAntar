using Microsoft.EntityFrameworkCore;

namespace API_Manajemen_Layanan_Pesan_Antar.Models

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.order_status)
                .HasConversion(os => os.ToString().Replace("_", " "), os => (Enums.OrderStatusEnum)Enum.Parse(typeof(Enums.OrderStatusEnum), os.Replace(" ", "_")));

            modelBuilder.Entity<Order>()
                .HasMany(o => o.order_items)
                .WithOne()
                .HasForeignKey(oi => oi.order_id);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.product)
                .WithMany()
                .HasForeignKey(oi => oi.product_id);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.order_delivery)
                .WithOne()
                .HasForeignKey<OrderDelivery>(od => od.order_id);
                
            modelBuilder.Entity<User>()
                .Property(r => r.role)
                .HasConversion(r => r.ToString().Replace("_", " "), r => (Enums.RoleEnum)Enum.Parse(typeof(Enums.RoleEnum), r.Replace(" ", "_")));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> Order_Items { get; set; }
        public DbSet<OrderDelivery> Order_Deliveries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FcmToken> Fcm_Token { get; set; }
    }
}
