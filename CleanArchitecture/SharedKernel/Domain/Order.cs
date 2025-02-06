namespace SharedKernel.Domain
{
	/// <summary>
	/// کلاس مربوط به سفارش (Order) که نشان‌دهنده یک سفارش در سیستم است.
	/// </summary>
	public class Order
	{
		/// <summary>
		/// شناسه یکتای سفارش (Primary Key)
		/// </summary>
		public Guid Id { get; private set; }

		/// <summary>
		/// لیستی از آیتم‌های موجود در سفارش
		/// </summary>
		public List<OrderItem> Items { get; private set; } = new List<OrderItem>();

		/// <summary>
		/// مجموع قیمت کل آیتم‌های سفارش
		/// </summary>
		public decimal TotalPrice { get; private set; }

		/// <summary>
		/// کانستراکتور پیش‌فرض برای EF Core (مورد نیاز برای کار با ORM)
		/// </summary>
		private Order() { }

		/// <summary>
		/// کانستراکتور کلاس سفارش که لیست آیتم‌ها را مقداردهی می‌کند
		/// </summary>
		/// <param name="items">لیست آیتم‌های سفارش</param>
		public Order(List<OrderItem> items)
		{
			Id = Guid.NewGuid(); // تولید شناسه یکتا برای سفارش جدید
			Items = items ?? throw new ArgumentNullException(nameof(items)); // بررسی مقدار نال بودن لیست آیتم‌ها
			TotalPrice = items.Sum(i => i.Price * i.Quantity); // محاسبه مجموع قیمت آیتم‌های سفارش
		}
	}
}
