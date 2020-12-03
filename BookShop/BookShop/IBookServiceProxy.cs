using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Core
{
	public interface IBookServiceProxy
	{
		Task<List<Book>> GetBooks(int count);
	}
}