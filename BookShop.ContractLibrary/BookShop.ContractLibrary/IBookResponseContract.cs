using System.Collections.Generic;

namespace BookShop.ContractLibrary
{
	public interface IBookResponseContract
	{
		List<IBook> Books { get; set; }
	}
}