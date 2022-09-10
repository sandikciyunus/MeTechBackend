using System;
using MediatR;
using MeTech.Domain.Entities;
using MeTech.Model.Backet;
using MeTech.ResponseRequest.Backet;
using Newtonsoft.Json;

namespace MeTech.Business.Handlers
{
	public class BacketAddCommandHandler:IRequestHandler<BacketAddRequest,BacketAddResponse>
	{
        private readonly MeTechContext context;
		public BacketAddCommandHandler(MeTechContext context)
		{
            this.context = context;
		}

        public async Task<BacketAddResponse> Handle(BacketAddRequest request, CancellationToken cancellationToken)
        {
            var response = new BacketAddResponse();
            List<BacketProductAddModel> products = new List<BacketProductAddModel>();
            try
            {
               
                for (int i = 0; i < request.Backet.Products.Count; i++)
                {
                    var product = context.Products.Where(p => p.Id == request.Backet.Products[i].Id).FirstOrDefault();
                    if (product == null)
                    {
                        response.ErrorMessage = "Sepette sistemde bulunmayan ürün var.";
                        response.IsSuccess = false;
                        return response;
                    }
                    BacketProductAddModel model = new BacketProductAddModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Count = request.Backet.Products[i].Count,
                        TotalPrice = product.Price * request.Backet.Products[i].Count
                    };
                    products.Add(model);
                  
                }
            
                var backet = new Backet
                {
                    Products = JsonConvert.SerializeObject(products)
                };
                await context.Backets.AddAsync(backet);
                context.SaveChanges();
                response.Id = backet.Id;
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

