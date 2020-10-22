using System.Net.Http;

namespace BookShop.Integration.ExternalServices
{
	public sealed class ServiceProxy
	{
		private readonly HttpClient _httpClient;

		public ServiceProxy(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
	}
}