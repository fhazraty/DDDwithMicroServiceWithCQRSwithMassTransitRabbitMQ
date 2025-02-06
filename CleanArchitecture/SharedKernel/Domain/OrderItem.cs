namespace SharedKernel.Domain
{
	/// <summary>
	/// کلاس مربوط به آیتم‌های سفارش که نشان‌دهنده یک محصول خاص در داخل سفارش است.
	/// </summary>
	public class OrderItem
	{
		/// <summary>
		/// شناسه محصول مرتبط با این آیتم سفارش
		/// </summary>
		public Guid ProductId { get; private set; }

		/// <summary>
		/// تعداد محصول خریداری شده
		/// </summary>
		public int Quantity { get; private set; }

		/// <summary>
		/// قیمت واحد محصول در زمان خرید
		/// </summary>
		public decimal Price { get; private set; }

		/// <summary>
		/// سازنده کلاس که مقدارهای اولیه را دریافت کرده و مقداردهی می‌کند.
		/// </summary>
		/// <param name="productId">شناسه محصول</param>
		/// <param name="quantity">تعداد محصول</param>
		/// <param name="price">قیمت واحد محصول</param>
		public OrderItem(Guid productId, int quantity, decimal price)
		{
			ProductId = productId; // مقداردهی شناسه محصول
			Quantity = quantity; // مقداردهی تعداد محصول
			Price = price; // مقداردهی قیمت واحد محصول
		}
	}
}
