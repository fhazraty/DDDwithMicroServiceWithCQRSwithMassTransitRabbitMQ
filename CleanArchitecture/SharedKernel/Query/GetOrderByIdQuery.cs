using MediatR;
using SharedKernel.Domain;

namespace SharedKernel.Query
{
	/// <summary>
	/// کلاس Query برای دریافت اطلاعات یک سفارش بر اساس شناسه (CQRS - Query)
	/// </summary>
	/// <remarks>
	/// این کلاس در الگوی CQRS به عنوان یک Query عمل می‌کند و درخواست دریافت اطلاعات یک سفارش را ارسال می‌کند.
	/// MediatR این Query را مدیریت کرده و نتیجه را از طریق یک Handler پردازش می‌کند.
	/// </remarks>
	public class GetOrderByIdQuery : IRequest<Order>
	{
		/// <summary>
		/// شناسه یکتای سفارش که باید جستجو شود
		/// </summary>
		public Guid OrderId { get; set; }

		/// <summary>
		/// سازنده کلاس که مقدار شناسه سفارش را دریافت و مقداردهی می‌کند
		/// </summary>
		/// <param name="orderId">شناسه سفارش</param>
		public GetOrderByIdQuery(Guid orderId)
		{
			OrderId = orderId;
		}
	}
}
