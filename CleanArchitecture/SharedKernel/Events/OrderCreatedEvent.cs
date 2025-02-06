namespace SharedKernel.Events
{
	/// <summary>
	/// رویداد (Event) مربوط به ایجاد سفارش جدید
	/// </summary>
	/// <remarks>
	/// این رویداد زمانی منتشر می‌شود که یک سفارش جدید در سیستم ثبت شود.
	/// سایر سرویس‌ها (مانند ProductService) می‌توانند این رویداد را دریافت کرده و واکنش مناسب نشان دهند.
	/// </remarks>
	public class OrderCreatedEvent
	{
		/// <summary>
		/// شناسه یکتای سفارش ایجاد شده
		/// </summary>
		public Guid OrderId { get; set; }

		/// <summary>
		/// لیستی از آیتم‌های موجود در این سفارش
		/// </summary>
		public List<OrderItemDto> Items { get; set; }
	}

	/// <summary>
	/// مدل داده‌ای برای نمایش آیتم‌های سفارش در رویداد
	/// </summary>
	/// <remarks>
	/// این کلاس برای ارسال اطلاعات هر آیتم سفارش در پیام‌های RabbitMQ یا سایر Message Brokers استفاده می‌شود.
	/// </remarks>
	public class OrderItemDto
	{
		/// <summary>
		/// شناسه محصول مرتبط با این آیتم سفارش
		/// </summary>
		public Guid ProductId { get; set; }

		/// <summary>
		/// تعداد محصول خریداری شده در سفارش
		/// </summary>
		public int Quantity { get; set; }
	}
}
