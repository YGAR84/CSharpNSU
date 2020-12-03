using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookShop.Core;
using Newtonsoft.Json;

namespace BookShop.Integration.ExternalServices
{
	public sealed class ServiceProxy : IBookServiceProxy
	{
		private readonly HttpClient _httpClient;
		private readonly string _endpoint = "https://getbooksrestapi.azurewebsites.net/api/books/";

		private class StupidBook
		{
			public int Id { get; set; }
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
				RequestUri = new Uri($"{_endpoint}/{count}"),

			};

			var response = await _httpClient.SendAsync(httpRequest);
			var books = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<List<StupidBook>>(books).Select(sb => sb.ToBook()).ToList();
		}
	}
}