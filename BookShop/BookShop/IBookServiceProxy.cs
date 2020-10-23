using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop
{
	public interface IBookServiceProxy
	{
		Task<List<Book>> GetBooks(int count);
	}
}