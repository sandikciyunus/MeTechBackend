using System;
using Microsoft.EntityFrameworkCore;

namespace MeTech.Domain.Entities
{
	public class MeTechContext:DbContext
	{
		public MeTechContext(DbContextOptions<MeTechContext> context):base(context)
		{
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Backet> Backets { get; set; }
		public DbSet<Campaign> Campaigns { get; set; }
		public DbSet<CampaignProduct> CampaignProducts { get; set; }
	}
}

