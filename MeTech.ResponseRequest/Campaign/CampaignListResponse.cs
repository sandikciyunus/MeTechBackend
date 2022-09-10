using System;
using MeTech.Model.Campaign;
using MeTech.ResponseRequest.Base;

namespace MeTech.ResponseRequest.Campaign
{
	public class CampaignListResponse:BaseResponse
	{
		public IList<CampaignListModel> Campaigns { get; set; }

		public CampaignListResponse()
		{
			Campaigns = new List<CampaignListModel>();
		}
	}
}

