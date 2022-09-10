using System;
using MediatR;
using MeTech.Domain.Entities;
using MeTech.Model.Campaign;
using MeTech.ResponseRequest.Campaign;

namespace MeTech.Business.Handlers
{
	public class CampaignListQueryHandler:IRequestHandler<CampaignListRequest,CampaignListResponse>
	{
        private readonly MeTechContext context;
		public CampaignListQueryHandler(MeTechContext context)
		{
            this.context = context;
		}

        public async Task<CampaignListResponse> Handle(CampaignListRequest request, CancellationToken cancellationToken)
        {
            var response = new CampaignListResponse();
            try
            {
                var campaigns = context.Campaigns.Where(p => p.IsDeleted == false).
                    Select(x => new CampaignListModel
                    {
                        Name = x.Name,
                        Code = x.Code
                    }).ToList();
                response.Campaigns = campaigns;
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

