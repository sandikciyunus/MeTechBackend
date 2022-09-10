using System;
using MeTech.Model.Product;
using MeTech.ResponseRequest.Base;

namespace MeTech.ResponseRequest.Product
{
	public class ProductListResponse:BaseResponse
	{
		public IList<ProductListModel> Products { get; set; }
		public ProductListResponse()
		{
			Products = new List<ProductListModel>();
		}
	}
}

