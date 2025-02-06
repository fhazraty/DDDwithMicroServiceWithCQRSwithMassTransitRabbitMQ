using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain;
using SharedKernel.Domain.EF;

namespace SharedKernel.Repository
{
	/// <summary>
	/// کلاس Repository برای مدیریت عملیات مرتبط با سفارشات (Order) در پایگاه داده
	/// </summary>
	/// <remarks>
	/// این کلاس از الگوی Repository استفاده می‌کند تا عملیات پایگاه داده از لایه دامنه جدا شود.
	/// همچنین از EF Core برای ارتباط با پایگاه داده استفاده می‌کند.
	/// </remarks>
	public class OrderRepository : IOrderRepository
	{
		/// <summary>
		/// نمونه‌ای از `OrderDbContext` برای تعامل با پایگاه داده
		/// </summary>
		private readonly OrderDbContext _context;

		/// <summary>
		/// سازنده کلاس که وابستگی به `OrderDbContext` را از طریق Dependency Injection دریافت می‌کند
		/// </summary>
		/// <param name="context">شیء `OrderDbContext` برای ارتباط با پایگاه داده</param>
		public OrderRepository(OrderDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// افزودن یک سفارش جدید به پایگاه داده
		/// </summary>
		/// <param name="order">شیء سفارش جدید</param>
		public async Task AddAsync(Order order)
		{
			await _context.Orders.AddAsync(order); // افزودن سفارش به پایگاه داده
			await _context.SaveChangesAsync(); // ذخیره تغییرات در پایگاه داده
		}

		/// <summary>
		/// دریافت یک سفارش بر اساس شناسه یکتا
		/// </summary>
		/// <param name="id">شناسه یکتای سفارش</param>
		/// <returns>اطلاعات سفارش (در صورت وجود)</returns>
		public async Task<Order> GetByIdAsync(Guid id)
		{
			return await _context.Orders
				.Include(o => o.Items) // بارگذاری تنبل (Lazy Loading) آیتم‌های سفارش
				.FirstOrDefaultAsync(o => o.Id == id); // جستجو بر اساس شناسه سفارش
		}
	}
}
