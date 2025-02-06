using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Command;
using SharedKernel.Query;

namespace OrderService.Controllers
{
	// تنظیم مسیر کلی API برای این کنترلر
	[Route("api/orders")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		// فیلد خصوصی برای MediatR جهت ارسال درخواست‌ها به هَندلرهای مربوطه
		private readonly IMediator _mediator;

		// تزریق وابستگی MediatR از طریق سازنده
		public OrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		/// ایجاد یک سفارش جدید
		/// </summary>
		/// <param name="command">داده‌های مربوط به سفارش جدید</param>
		/// <returns>شناسه سفارش ایجاد شده</returns>
		[HttpPost]
		public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
		{
			// ارسال دستور ایجاد سفارش به MediatR و دریافت شناسه سفارش ایجاد شده
			var orderId = await _mediator.Send(command);

			// برگرداندن نتیجه با کد 201 (Created) و لینک دریافت سفارش جدید ایجاد شده
			return CreatedAtAction(nameof(GetOrder), new { id = orderId }, null);
		}

		/// <summary>
		/// دریافت اطلاعات یک سفارش بر اساس شناسه
		/// </summary>
		/// <param name="id">شناسه سفارش</param>
		/// <returns>اطلاعات سفارش</returns>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrder(Guid id)
		{
			// ارسال درخواست دریافت سفارش به MediatR
			var order = await _mediator.Send(new GetOrderByIdQuery(id));

			// اگر سفارش یافت شد، آن را برمی‌گرداند؛ در غیر این صورت، کد 404 (Not Found) بازمی‌گرداند
			return order != null ? Ok(order) : NotFound();
		}
	}
}
