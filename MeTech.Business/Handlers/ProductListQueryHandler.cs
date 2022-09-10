using System;
using MediatR;
using MeTech.Domain.Entities;
using MeTech.Model.Product;
using MeTech.ResponseRequest.Product;

namespace MeTech.Business.Handlers
{
	public class ProductListQueryHandler:IRequestHandler<ProductListRequest,ProductListResponse>
	{
        private readonly MeTechContext context;
		public ProductListQueryHandler(MeTechContext context)
		{
            this.context = context;
		}

        public async Task<ProductListResponse> Handle(ProductListRequest request, CancellationToken cancellationToken)
        {
            var response = new ProductListResponse();
            try
            {
                var products = from p in context.Products
                               join c in context.Categories on p.CategoryId equals c.Id
                               where p.IsDeleted == false
                               select new ProductListModel
                               {
                                   Name = p.Name,
                                   Price = p.Price,
                                   CategoryName = c.Name,
                                   Stock=p.Stock
                               };
                response.Products = products.ToList();
                response.IsSuccess = true;

            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }
    }
}

