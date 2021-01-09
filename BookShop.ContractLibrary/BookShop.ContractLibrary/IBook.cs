using System;

namespace BookShop.ContractLibrary
{
	public interface IBook
	{
		public string Title { get; set; }
		public string Genre { get; set; }
		public int Price { get; set; }
		public bool IsNew { get; set; }
		public DateTime DateOfDelivery { get; set; }
	}
}
