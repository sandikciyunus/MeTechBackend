using System;
namespace MeTech.Model.Backet
{
	public class BacketProductAddModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Count { get; set; }
		public decimal TotalPrice { get; set; }
	}
}

