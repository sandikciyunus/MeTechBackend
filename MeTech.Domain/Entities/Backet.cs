using System;
using MeTech.Domain.Entities.Base;

namespace MeTech.Domain.Entities
{
	public class Backet:BaseEntity
	{
		public string Products { get; set; }
		public int CampaignId { get; set; }
	}
}

