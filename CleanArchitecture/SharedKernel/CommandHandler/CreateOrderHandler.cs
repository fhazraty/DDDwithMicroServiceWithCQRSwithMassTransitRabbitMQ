using MassTransit;
using MediatR;
using SharedKernel.Command;
using SharedKernel.Domain;
using SharedKernel.Events;
using SharedKernel.Repository;

namespace SharedKernel.CommandHandler
{
	/// <summary>
	/// هندلر مربوط به فرمان ایجاد سفارش (CQRS - Command Handler)
	/// </summary>
	/// <remarks>
	/// این کلاس از IRequestHandler<CreateOrderCommand, Guid> وراثت گرفته است که MediatR را قادر می‌سازد این فرمان را پردازش کند.
	/// خروجی این فرمان یک شناسه (Guid) برای سفارش ایجاد شده خواهد بود.
	/// </remarks>
	public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
	{
		// وابستگی به ریپازیتوری سفارش برای ذخیره اطلاعات در پایگاه داده
		private readonly IOrderRepository _orderRepository;

		// MassTransit برای انتشار رویداد در RabbitMQ
		private readonly IPublishEndpoint _publishEndpoint;

		/// <summary>
		/// سازنده‌ی کلاس که وابستگی‌های لازم را از طریق DI دریافت می‌کند
		/// </summary>
		/// <param name="orderRepository">ریپازیتوری سفارش</param>
		/// <param name="publishEndpoint">انتشار‌دهنده‌ی پیام در RabbitMQ</param>
		public CreateOrderHandler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint)
		{
			_orderRepository = orderRepository;
			_publishEndpoint = publishEndpoint;
		}

		/// <summary>
		/// پردازش فرمان ایجاد سفارش
		/// </summary>
		/// <param name="request">داده‌های ارسال شده برای ایجاد سفارش</param>
		/// <param name="cancellationToken">توکن کنسل کردن عملیات</param>
		/// <returns>شناسه‌ی سفارش ایجاد شده</returns>
		public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			// ایجاد یک شیء جدید از نوع Order بر اساس داده‌های دریافتی
			var order = new Order(request.Items);

			// ذخیره سفارش در پایگاه داده
			await _orderRepository.AddAsync(order);

			// ساخت و انتشار یک رویداد (Event) برای اطلاع سایر سرویس‌ها از ایجاد یک سفارش جدید
			var orderEvent = new OrderCreatedEvent
			{
				OrderId = order.Id,
				Items = order.Items.Select(i => new OrderItemDto
				{
					ProductId = i.ProductId,
					Quantity = i.Quantity
				}).ToList()
			};

			// ارسال Event به RabbitMQ برای اطلاع سایر میکروسرویس‌ها
			await _publishEndpoint.Publish(orderEvent, cancellationToken);

			// بازگرداندن شناسه‌ی سفارش ایجاد شده
			return order.Id;
		}
	}
}
