using System;
using MeTech.Domain.Entities.Base;

namespace MeTech.Domain.Entities
{
	public class CampaignProduct:BaseEntity
	{
        public int CampaignId { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
        public int Rate { get; set; }
    }
}

