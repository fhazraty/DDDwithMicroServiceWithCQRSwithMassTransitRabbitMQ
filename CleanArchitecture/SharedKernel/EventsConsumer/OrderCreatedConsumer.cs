using MassTransit;
using SharedKernel.Events;

namespace SharedKernel.EventsConsumer
{
	/// <summary>
	/// مصرف‌کننده (Consumer) برای رویداد ایجاد سفارش جدید
	/// </summary>
	/// <remarks>
	/// این کلاس مسئول دریافت رویداد `OrderCreatedEvent` از RabbitMQ است.
	/// هنگام دریافت یک سفارش جدید، عملیات مربوطه مانند کاهش موجودی محصول یا ثبت لاگ انجام می‌شود.
	/// </remarks>
	public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
	{
		/// <summary>
		/// متد Consume برای پردازش پیام‌های دریافتی از RabbitMQ
		/// </summary>
		/// <param name="context">اطلاعات مربوط به پیام دریافتی</param>
		public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
		{
			// دریافت پیام و استخراج داده‌های مربوط به سفارش
			var orderEvent = context.Message;

			// نمایش اطلاعات سفارش در لاگ (در محیط واقعی باید در دیتابیس ذخیره شود)
			Console.WriteLine($"📦 سفارش جدید دریافت شد: OrderId = {orderEvent.OrderId}");

			// 🛠 در این بخش می‌توان عملیات مرتبط با سفارش را انجام داد، از جمله:
			// - کاهش موجودی محصول در ProductService
			// - ارسال ایمیل تأیید سفارش به مشتری
			// - ذخیره اطلاعات سفارش در دیتابیس لاگ‌های سیستم
			// - ارسال پیام به سرویس‌های دیگر برای پردازش بیشتر

			// شبیه‌سازی یک عملیات آسنکرون (مثلاً به‌روزرسانی موجودی محصول در پایگاه داده)
			await Task.CompletedTask;
		}
	}
}
