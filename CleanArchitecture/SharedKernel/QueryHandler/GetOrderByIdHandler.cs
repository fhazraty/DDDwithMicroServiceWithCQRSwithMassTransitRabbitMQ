using MediatR;
using SharedKernel.Domain;
using SharedKernel.Query;
using SharedKernel.Repository;

namespace SharedKernel.QueryHandler
{
	/// <summary>
	/// هندلر (Query Handler) برای دریافت اطلاعات یک سفارش بر اساس شناسه (CQRS - Query Handler)
	/// </summary>
	/// <remarks>
	/// این کلاس وظیفه دارد Query مربوط به دریافت سفارش (`GetOrderByIdQuery`) را پردازش کند.
	/// این عملیات از طریق MediatR مدیریت شده و با استفاده از Repository اطلاعات سفارش از پایگاه داده بازیابی می‌شود.
	/// </remarks>
	public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order>
	{
		/// <summary>
		/// ریپازیتوری سفارش که برای دریافت اطلاعات از پایگاه داده استفاده می‌شود
		/// </summary>
		private readonly IOrderRepository _orderRepository;

		/// <summary>
		/// سازنده کلاس که وابستگی به `IOrderRepository` را از طریق Dependency Injection دریافت می‌کند
		/// </summary>
		/// <param name="orderRepository">ریپازیتوری مربوط به سفارشات</param>
		public GetOrderByIdHandler(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		/// <summary>
		/// متد پردازش Query برای دریافت یک سفارش بر اساس شناسه
		/// </summary>
		/// <param name="request">درخواست دریافت سفارش که شامل `OrderId` است</param>
		/// <param name="cancellationToken">توکن برای مدیریت لغو عملیات</param>
		/// <returns>اطلاعات سفارش (در صورت یافت شدن)</returns>
		public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
		{
			// دریافت سفارش از پایگاه داده بر اساس شناسه
			return await _orderRepository.GetByIdAsync(request.OrderId);
		}
	}
}
