using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Core;
using BookShop.Logic.Requests;
using BookShop.Logic.Requests.BookRequests;

namespace BookShop.Logic
{
	public interface IBookServiceProxy
	{
		Task<List<AddBookRequest>> GetAddBookRequests(int count);
		Task<List<Book>> GetBooks(int count);
	}
}