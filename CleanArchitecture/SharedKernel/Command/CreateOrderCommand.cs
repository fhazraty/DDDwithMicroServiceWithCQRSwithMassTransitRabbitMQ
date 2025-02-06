using MediatR;
using SharedKernel.Domain;

namespace SharedKernel.Command
{
	/// <summary>
	/// کلاس فرمان (Command) برای ایجاد سفارش جدید
	/// </summary>
	/// <remarks>
	/// این کلاس از IRequest<Guid> وراثت گرفته است که به MediatR می‌گوید خروجی این فرمان یک Guid (شناسه سفارش) خواهد بود.
	/// </remarks>
	public class CreateOrderCommand : IRequest<Guid>
	{
		/// <summary>
		/// لیستی از آیتم‌های سفارش
		/// </summary>
		public List<OrderItem> Items { get; set; }

		/// <summary>
		/// سازنده کلاس که لیست آیتم‌های سفارش را مقداردهی می‌کند
		/// </summary>
		/// <param name="items">لیست محصولات داخل سفارش</param>
		public CreateOrderCommand(List<OrderItem> items)
		{
			Items = items;
		}
	}
}
