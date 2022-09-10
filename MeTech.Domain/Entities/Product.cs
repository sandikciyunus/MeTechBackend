using System;
using MeTech.Domain.Entities.Base;

namespace MeTech.Domain.Entities
{
	public class Product:BaseEntity
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int CategoryId { get; set; }
		public int Stock { get; set; }
	}
}

