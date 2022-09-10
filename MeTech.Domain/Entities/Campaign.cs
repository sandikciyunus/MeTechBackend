using System;
using MeTech.Domain.Entities.Base;

namespace MeTech.Domain.Entities
{
	public class Campaign:BaseEntity
	{
		public string Name { get; set; }
		public int Rate { get; set; }
        public string Code { get; set; }
		public decimal Price { get; set; }
		public bool IsProductsRequired { get; set; }
		public bool AllProductsRequired { get; set; }
		public bool IsGeneralDiscountPrice { get; set; }
	}
}

