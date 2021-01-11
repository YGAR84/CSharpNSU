using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookShop.ContractLibrary;
using BookShop.Core;
using BookShop.Logic;
using BookShop.Logic.Requests;
using BookShop.Logic.Requests.BookRequests;
using Newtonsoft.Json;

namespace BookShop.Integration.ExternalServices
{
	public sealed class ServiceProxy : IBookServiceProxy
	{
		private readonly HttpClient _httpClient;
		private const string Endpoint = "https://getbooksrestapi.azurewebsites.net/api/books/";

		#warning приватный класс лучше всё-таки размещать в конце, после всех паблик членов
		private class AzureBook : IBook
		{
			public string Title { get; set; }
			public string Genre { get; set; }
			public int Price { get; set; }
			public bool IsNew { get; set; }
			public DateTime DateOfDelivery { get; set; }

			public Book ToBook()
			{
				var bookInfo = new BookInfo(Title, "no author", new List<Genre>{ new Genre(Genre)});
				var book = new Book(bookInfo, Price, DateOfDelivery);
				return book;
			}

			public AddBookRequest ToAddBookRequest()
			{
				return new AddBookRequest()
				{
					ArriveDate = DateOfDelivery,
					Author = "no author",
					Cost = Price,
					Genres = new List<string> {Genre},
					Title = Title
				};
			}
		}

		public ServiceProxy(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<Book>> GetBooks(int count)
		{
			var httpRequest = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"{Endpoint}/{count}"),

			};

			var response = await _httpClient.SendAsync(httpRequest);
			var books = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<List<AzureBook>>(books).Select(sb => sb.ToBook()).ToList();
		}

		public async Task<List<AddBookRequest>> GetAddBookRequests(int count)
		{
			var httpRequest = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"{Endpoint}/{count}"),

			};

			var response = await _httpClient.SendAsync(httpRequest);
			var booksString = await response.Content.ReadAsStringAsync();
			var books = JsonConvert.DeserializeObject<List<AzureBook>>(booksString);
			return books.Select(sb => sb.ToAddBookRequest()).ToList();
		}
	}
}