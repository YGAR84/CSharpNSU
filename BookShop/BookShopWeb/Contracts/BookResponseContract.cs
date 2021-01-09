using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.ContractLibrary;

namespace BookShop.Web.Contracts
{
	public class BookResponseContract : IBookResponseContract
	{
		public List<IBook> Books { get; set; }
	}
}
