using Microsoft.EntityFrameworkCore;
using MassTransit;
using SharedKernel.Domain.EF;
using SharedKernel.Repository;

// نصب MassTransit برای مدیریت ارتباطات پیام‌رسانی
// Install-Package MassTransit

var builder = WebApplication.CreateBuilder(args);

// اضافه کردن سرویس‌های کنترلرها به DI Container
builder.Services.AddControllers();

// نصب و تنظیم Swagger برای مستندسازی API
// Install-Package Swashbuckle.AspNetCore
builder.Services.AddSwaggerGen();

// اضافه کردن MediatR به DI Container برای مدیریت CQRS
builder.Services.AddMediatR(cfg =>
	cfg.RegisterServicesFromAssembly(typeof(SharedKernel.CommandHandler.CreateOrderHandler).Assembly));

// تنظیمات EF Core برای اتصال به پایگاه داده
builder.Services.AddDbContext<OrderDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ثبت Repository در DI Container برای مدیریت دسترسی به پایگاه داده
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// تنظیم MassTransit برای ارتباط با RabbitMQ
builder.Services.AddMassTransit(busConfigurator =>
{
	busConfigurator.UsingRabbitMq((context, rabbitMqConfigurator) =>
	{
		// مشخص کردن هاست RabbitMQ و اطلاعات ورود
		rabbitMqConfigurator.Host("localhost", h =>
		{
			h.Username("guest");
			h.Password("guest");
		});
	});
});

// ساخت و اجرای اپلیکیشن
var app = builder.Build();

// اگر برنامه در محیط Development اجرا می‌شود، Swagger را فعال کن
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// تنظیم مسیرهای API
app.MapControllers();

// اجرای برنامه
app.Run();
