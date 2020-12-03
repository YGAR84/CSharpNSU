using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookShopWeb
{
	#warning нет контроллера для книг с CRUD-операциями
	#warning нет EFa
	#warning нет джобов
	#warning нет RMQ 
	#warning за тесты плюсик
	#warning ещё раз дублирую полное описание, что должно получиться 
	/*
	 * 1е приложение: это веб-приложение, в ядре (Core часть, доменная модель) которого сам bookshop - нечто, что умеет принимать/продавать книги,
	 * следить за состоянием коллекции этих книг, запускать распродажи и тп. Это приложение работает с базой.
В этом приложении есть апи, которое позволяет купить книгу и получить текущий список имеющихся книг.  
В этом приложении есть джобик, который с некоторой периодичностью смотрит, нужно ли заказать новую партию книг, 
и если надо, то кидает в rabbit сообщение для второго приложения (в контракте может передаваться, например, кол-во книг, которое нужно запросить). 

2е приложение:  это может быть так же веб-приложение. В нём есть консьюмер, который принимает сообщение от первого приложения на запрос книг, 
делает запрос во внешнее api, получает оттуда нужное кол-во книг и отправляет в rabbit другое сообщение, в котором передаются данные по книгам. 

В 1м приложении для этого сообщения должен быть так же консюмер, который принимает это сообщение и пытается принять новую партию книг.  */
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
