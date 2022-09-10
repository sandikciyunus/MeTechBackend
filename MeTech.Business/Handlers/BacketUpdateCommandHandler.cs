using System;
using MediatR;
using MeTech.Domain.Entities;
using MeTech.Model.Backet;
using MeTech.ResponseRequest.Backet;
using Newtonsoft.Json;

namespace MeTech.Business.Handlers
{
	public class BacketUpdateCommandHandler:IRequestHandler<BacketUpdateRequest,BacketUpdateResponse>
	{
        private readonly MeTechContext context;
		public BacketUpdateCommandHandler(MeTechContext context)
		{
            this.context = context;
		}

        public async Task<BacketUpdateResponse> Handle(BacketUpdateRequest request, CancellationToken cancellationToken)
        {
            var response = new BacketUpdateResponse();
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

                var backet = context.Backets.Where(p => p.Id == request.Backet.Id).FirstOrDefault();
                backet.Products = JsonConvert.SerializeObject(products);
                 context.Backets.Update(backet);
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

