using Microsoft.EntityFrameworkCore;

namespace HotelManageSystem.Models
{
    public class HotelDbContext : DbContext
    {
        // Constructor bắt buộc để nhận cấu hình kết nối từ Program.cs
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
        }

        // Định nghĩa các bảng dữ liệu trong DB
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình các ràng buộc đặc biệt (Fluent API) như thuộc tính Unique trong thiết kế của bạn
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.RoleName)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Username)
                .IsUnique();
        }
    }

    // --- Khung thực thể tạm thời phục vụ kết nối (Sẽ mở rộng chi tiết sau) ---
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }

    public class Account
    {
        public int AccountId { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;
    }
}