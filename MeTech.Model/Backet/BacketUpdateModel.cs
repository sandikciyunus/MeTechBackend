using System;
namespace MeTech.Model.Backet
{
	public class BacketUpdateModel
	{
        public int Id { get; set; }
        public IList<BacketProductAddModel> Products { get; set; }
        public BacketUpdateModel()
        {
            Products = new List<BacketProductAddModel>();
        }
    }
}

