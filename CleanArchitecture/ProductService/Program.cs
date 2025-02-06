using MassTransit;
using SharedKernel.EventsConsumer;

// نصب MassTransit و MassTransit.RabbitMQ برای ارتباط با RabbitMQ
// Install-Package MassTransit
// Install-Package MassTransit.RabbitMQ

var builder = WebApplication.CreateBuilder(args);

// اضافه کردن سرویس‌های مربوط به کنترلرهای API
builder.Services.AddControllers();

// نصب و تنظیم Swagger برای مستندسازی API
// Install-Package Swashbuckle.AspNetCore
builder.Services.AddSwaggerGen();

// راه‌اندازی RabbitMQ از طریق Docker (در صورت نیاز)
// docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management

// تنظیمات MassTransit برای ارتباط با RabbitMQ
builder.Services.AddMassTransit(busConfigurator =>
{
	// ثبت Consumer مربوط به دریافت پیام‌های سفارش جدید
	busConfigurator.AddConsumer<OrderCreatedConsumer>();

	busConfigurator.UsingRabbitMq((context, rabbitMqConfigurator) =>
	{
		// تنظیم آدرس و اطلاعات احراز هویت برای اتصال به RabbitMQ
		rabbitMqConfigurator.Host("localhost", h =>
		{
			h.Username("guest");
			h.Password("guest");
		});

		// تعریف یک صف (Queue) برای دریافت پیام‌های مربوط به سفارشات جدید
		rabbitMqConfigurator.ReceiveEndpoint("order-created-queue", e =>
		{
			e.ConfigureConsumer<OrderCreatedConsumer>(context);
		});
	});
});

var app = builder.Build();

// اگر محیط توسعه (Development) است، Swagger را فعال کن
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// تنظیم مسیرهای API
app.MapControllers();

// اجرای برنامه
app.Run();
