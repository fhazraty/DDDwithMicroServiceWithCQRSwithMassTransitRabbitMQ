using SharedKernel.Domain;

namespace SharedKernel.Repository
{
	/// <summary>
	/// اینترفیس Repository برای مدیریت عملیات مربوط به سفارشات (Order)
	/// </summary>
	/// <remarks>
	/// این اینترفیس به عنوان یک لایه انتزاعی بین لایه دامنه و پایگاه داده عمل می‌کند.
	/// پیاده‌سازی این اینترفیس در کلاس `OrderRepository` انجام می‌شود.
	/// </remarks>
	public interface IOrderRepository
	{
		/// <summary>
		/// افزودن یک سفارش جدید به پایگاه داده
		/// </summary>
		/// <param name="order">شیء سفارش که باید اضافه شود</param>
		/// <returns>یک `Task` که نشان‌دهنده عملیات آسنکرون است</returns>
		Task AddAsync(Order order);

		/// <summary>
		/// دریافت یک سفارش بر اساس شناسه یکتا
		/// </summary>
		/// <param name="id">شناسه یکتای سفارش</param>
		/// <returns>شیء سفارش (در صورت یافت شدن) یا مقدار `null`</returns>
		Task<Order> GetByIdAsync(Guid id);
	}
}
