using System;
using MediatR;
using MeTech.Domain.Entities;
using MeTech.Model.Backet;
using MeTech.ResponseRequest.Backet;
using System.Text.Json;


namespace MeTech.Business.Handlers
{
	public class BacketGetQueryHandler:IRequestHandler<BacketGetRequest,BacketGetResponse>
	{
        private readonly MeTechContext context;
		public BacketGetQueryHandler(MeTechContext context)
		{
            this.context = context;
		}

        public async Task<BacketGetResponse> Handle(BacketGetRequest request, CancellationToken cancellationToken)
        {
            var response = new BacketGetResponse();
            try
            {
                var backet = context.Backets.Where(p => p.IsDeleted == false && p.Id == request.Id)
                    .Select(x=>new BacketGetModel
                    {
                        Id=x.Id,
                        CampaignId=x.CampaignId,
                        Products=x.Products
                    }).FirstOrDefault();
                var campaign = context.Campaigns.Where(p => p.IsDeleted == false).ToList();
                if (backet == null)
                {
                    response.ErrorMessage = "Sepet bulunamadı.";
                    response.IsSuccess = false;
                    return response;
                }
                backet.Campaign = campaign.Find(p => p.Id == backet.CampaignId);
                var productList= JsonSerializer.Deserialize<List<BacketProductAddModel>>(backet.Products);
               backet.ProductList = productList;
                for (int i = 0; i < productList.Count; i++)
                {
                    backet.GeneralPrice += productList[i].TotalPrice;
                }
                response.Backet = backet;
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

