using System;
namespace MeTech.Model.Backet
{
	public class BacketAddModel
	{
		public IList<BacketProductAddModel> Products { get; set; }
		public BacketAddModel()
		{
			Products = new List<BacketProductAddModel>();
		}
	}
}

