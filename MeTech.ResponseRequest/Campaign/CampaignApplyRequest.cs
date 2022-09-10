using System;
using MediatR;
using MeTech.Model.Campaign;

namespace MeTech.ResponseRequest.Campaign
{
	public class CampaignApplyRequest:IRequest<CampaignApplyResponse>
	{
		public CampaignApplyModel Campaign { get; set; }
	}
}

