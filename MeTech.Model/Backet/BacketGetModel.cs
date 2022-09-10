using System;
namespace MeTech.Model.Backet
{
	public class BacketGetModel
	{
		public int Id { get; set; }
		public string Products { get; set; }
		public IList<BacketProductAddModel> ProductList{ get; set; }
		public BacketGetModel()
		{
            ProductList = new List<BacketProductAddModel>();
		}
		public decimal GeneralPrice { get; set; }
		public int CampaignId { get; set; }
		public MeTech.Domain.Entities.Campaign Campaign { get; set; }
	}
}

