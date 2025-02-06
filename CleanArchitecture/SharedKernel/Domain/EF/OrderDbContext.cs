using Microsoft.EntityFrameworkCore;

/*
 * 📌 تنظیمات اولیه EF Core
 * 
 * 🛠 دستورات نصب EF Core:
 * Install-Package Microsoft.EntityFrameworkCore.SqlServer
 * Install-Package Microsoft.EntityFrameworkCore.Tools
 * Install-Package Microsoft.EntityFrameworkCore.Design
 * Install-Package Microsoft.EntityFrameworkCore.Proxies
 * 
 * 🔧 دستورات مربوط به Migration:
 * Add-Migration InitialCreate -Context "OrderDbContext"    // ایجاد اولین Migration
 * Update-Database -Context "OrderDbContext"               // به‌روزرسانی پایگاه داده با آخرین Migration
 * Script-Migration -Context "OrderDbContext"              // استخراج اسکریپت SQL از Migrationها
 */

namespace SharedKernel.Domain.EF
{
	/// <summary>
	/// کلاس DbContext برای مدیریت ارتباط با پایگاه داده مربوط به سفارشات
	/// </summary>
	public class OrderDbContext : DbContext
	{
		/// <summary>
		/// جدول سفارشات در پایگاه داده
		/// </summary>
		public DbSet<Order> Orders { get; set; }

		/// <summary>
		/// سازنده پیش‌فرض (مورد نیاز برای برخی سناریوهای EF Core مانند طراحی)
		/// </summary>
		public OrderDbContext() { }

		/// <summary>
		/// سازنده‌ای که تنظیمات مربوط به اتصال به پایگاه داده را دریافت می‌کند
		/// </summary>
		/// <param name="options">تنظیمات اتصال به پایگاه داده</param>
		public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

		/// <summary>
		/// پیکربندی اتصال به پایگاه داده در صورت عدم مقداردهی در DI
		/// </summary>
		/// <param name="optionsBuilder">ابزار تنظیمات EF Core</param>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				// تنظیمات پیش‌فرض برای اتصال به SQL Server (در صورت عدم مقداردهی در DI)
				optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ProductDatabase;Integrated Security=True;Trust Server Certificate=True");
			}
		}

		/// <summary>
		/// پیکربندی مدل‌های دیتابیس و تعیین کلیدهای اصلی جداول
		/// </summary>
		/// <param name="modelBuilder">ابزار مدل‌سازی پایگاه داده</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// تعیین کلید اصلی جدول سفارشات
			modelBuilder.Entity<Order>().HasKey(o => o.Id);

			// تعیین کلید اصلی برای جدول آیتم‌های سفارش
			modelBuilder.Entity<OrderItem>().HasKey(oi => new { oi.ProductId });
		}
	}
}
