using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookShop.ContractLibrary;
using Newtonsoft.Json;

namespace BookShop.BooksProvider.ExternalServices
{
	public sealed class ServiceProxy
	{
		private readonly HttpClient _httpClient;
		private const string Endpoint = "https://getbooksrestapi.azurewebsites.net/api/books/";

		private class AzureBook : IBook
		{
			public string Title { get; set; }
			public string Genre { get; set; }
			public int Price { get; set; }
			public bool IsNew { get; set; }
			public DateTime DateOfDelivery { get; set; }
		}

		public ServiceProxy(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<IBook>> GetBooks(int count)
		{
			var httpRequest = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"{Endpoint}/{count}"),

			};

			var response = await _httpClient.SendAsync(httpRequest);
			var books = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<List<AzureBook>>(books)
				.Select(b => (IBook)b)
				.ToList();
		}

	}
}