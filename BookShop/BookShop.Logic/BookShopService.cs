using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.ContractLibrary;
using BookShop.Core;
using BookShop.Core.Discounts;
using BookShop.Infrastructure.EntityFramework;
using BookShop.Logic.Extensions;
using BookShop.Logic.Requests.BookRequests;
using BookShop.Logic.Requests.BookShopRequest;
using BookShop.Logic.Requests.DiscountsRequests;
using BookShop.Logic.Responses.BookResponses;
using BookShop.Logic.Responses.BookShopResponses;
using BookShop.Logic.Responses.DiscountsResponses;
using BookShop.Logic.Utils.GetDiscountResponseBuilder;

namespace BookShop.Logic
{
#warning Не нравится, что класс так разросся, но не знаю, как красиво раскидать)
	public class BookShopService
	{
		private readonly BookShopContextDbContextFactory _dbContextFactory;

		private readonly int _bookShopStateId;
		private const decimal AcceptanceBookPercent = 0.07M;

		public BookShopService(BookShopContextDbContextFactory dbContextFactory, int bookShopStateId)
		{
			_dbContextFactory = dbContextFactory;
			_bookShopStateId = bookShopStateId;
		}

		public async Task AddBook(AddBookRequest addBookRequest)
		{
			var bookShopState = await GetBookShopState();
			if (bookShopState.StorageSize <= await _dbContextFactory.GetContext().GetBooksCount()) return;

			var bookCost = GetBookAcceptanceCost(addBookRequest.Cost);
			if (bookCost > bookShopState.Balance) return;

			bookShopState.Balance -= bookCost;

			var transaction = await _dbContextFactory.GetContext().Database.BeginTransactionAsync();
			try
			{
				await _dbContextFactory.GetContext().UpdateBookShopState(bookShopState);
				await MapAddBookRequestToBook(addBookRequest);
			}
			catch (Exception)
			{
				// ignored
			}
		}

		public async Task AddBooks(List<AddBookRequest> addBookRequests)
		{
			foreach (var addBookRequest in addBookRequests)
			{
				await AddBook(addBookRequest);
			}
		}

		public async Task AddBooks(List<IBook> books)
		{
			var addBookRequests = books
				.Select(b => b.ToAddBookRequest())
				.ToList();
			await AddBooks(addBookRequests);
		}

		private static decimal GetBookAcceptanceCost(decimal bookCost)
		{
			return bookCost * AcceptanceBookPercent;
		}

		public async Task<GetBookResponse> GetBookResponse(Guid guid)
		{
			var book = await GetBook(guid);
			return GetBookResponse(book);
		}

		public GetBookResponse GetBookResponse(Book book)
		{
			return book == null ? null : MapBookToGetBookResponse(book);
		}

		public async Task<List<GetBooksResponse>> GetBooksResponse()
		{
			var bookInfos = await _dbContextFactory.GetContext().GetBookInfos();
			return bookInfos.Select(MapBookInfoToGetBooksResponse).ToList();
		}

		public async Task DeleteBook(Guid bookGuid)
		{
			await _dbContextFactory.GetContext().DeleteBook(bookGuid);
		}

		public async Task SellBook(Guid bookGuid)
		{
			var bookShopState = await GetBookShopState();

			var book = await GetBookWithDiscount(bookGuid);
			bookShopState.Balance += book.Cost;

			var transaction = await _dbContextFactory.GetContext().Database.BeginTransactionAsync();
			try
			{
				await _dbContextFactory.GetContext().UpdateBookShopState(bookShopState);
				await _dbContextFactory.GetContext().DeleteBook(bookGuid);

				await transaction.CommitAsync();
			}
			catch (Exception)
			{
				// ignored
			}
		}

		public async Task UpdateBook(Guid bookGuid, UpdateBookRequest updateBookRequest)
		{
			await MapUpdateBookRequestToBook(bookGuid, updateBookRequest);
		}

		private async Task<Book> MapAddBookRequestToBook(AddBookRequest addBookRequest)
		{
			List<Genre> genres = new List<Genre>();
			foreach (var genreName in addBookRequest.Genres)
			{
				Genre genre = await _dbContextFactory
					.GetContext()
					.GetOrCreateGenre(genreName);
				genres.Add(genre);
			}

			BookInfo bookInfo = await _dbContextFactory
				.GetContext()
				.GetOrCreateBookInfo(addBookRequest.Author, addBookRequest.Title, genres);

			Book book = new Book
			{
				ArriveDate = addBookRequest.ArriveDate,
				BookInfoId = bookInfo.Id,
				Cost = addBookRequest.Cost
			};

			return await _dbContextFactory
				.GetContext()
				.AddBook(book);
		}

		private async Task<Book> MapUpdateBookRequestToBook(Guid bookGuid, UpdateBookRequest updateBookRequest)
		{
			var bookShopState = await GetBookShopState();

			var book = await _dbContextFactory.GetContext().GetBook(bookGuid);
			if (book == null)
			{
				return null;
			}

			if (updateBookRequest.Title != book.BookInfo.Title || updateBookRequest.Author != book.BookInfo.Author)
			{
				List<Genre> genres = new List<Genre>();
				if (updateBookRequest.Genres != null)
				{
					foreach (var genreName in updateBookRequest.Genres)
					{
						Genre genre = await _dbContextFactory
							.GetContext()
							.GetOrCreateGenre(genreName);

						genres.Add(genre);
					}
				}

				BookInfo bookInfo = await _dbContextFactory
					.GetContext()
					.GetOrCreateBookInfo(updateBookRequest.Author, updateBookRequest.Title, genres);

				book.BookInfoId = bookInfo.Id;
			}

			book.BookInfo = null;

			book.Cost = updateBookRequest.Cost;
			book.ArriveDate = updateBookRequest.ArriveDate;

			return await _dbContextFactory
				.GetContext()
				.UpdateBook(book);
		}

		private GetBookResponse MapBookToGetBookResponse(Book book)
		{
			return new GetBookResponse()
			{
				ArriveDate = book.ArriveDate,
				Author = book.BookInfo.Author,
				Cost = book.Cost,
				BookInfoId = book.BookInfoId,
				Genres = book.BookInfo.Genres.Select(g => g.Name).ToList(),
				Guid = book.Guid,
				Title = book.BookInfo.Title
			};
		}

		private GetBooksResponse MapBookInfoToGetBooksResponse(BookInfo bookInfo)
		{
			return new GetBooksResponse()
			{

				BookInfoId = bookInfo.Id,
				Author = bookInfo.Author,
				Title = bookInfo.Title,
				Genres = bookInfo.Genres.Select(g => g.Name).ToList(),
				Books = bookInfo.Books.Select(book => new InnerBook()
				{
					Guid = book.Guid,
					ArriveDate = book.ArriveDate,
					Cost = book.Cost
				}).ToList()
			};
		}

		private async Task<Book> ApplyDiscounts(Book book)
		{
			var discounts = await GetDiscounts(book);

			foreach (var discount in discounts)
			{
				book = discount.ApplyDiscount(book, DateTime.Now);
			}

			return book;
		}

		private async Task<Book> GetBook(Guid bookGuid)
		{
			return await _dbContextFactory.GetContext().GetBook(bookGuid);
		}

		private async Task<Book> GetBookWithDiscount(Book book)
		{
			book = await ApplyDiscounts(book);
			return book;
		}

		private async Task<Book> GetBookWithDiscount(Guid bookGuid)
		{
			var book = await GetBook(bookGuid);
			return await GetBookWithDiscount(book);
		}

		private async Task<List<Discount>> GetDiscounts(Book book)
		{
			List<Discount> discounts = new List<Discount>();

			var defectDiscounts = await _dbContextFactory
				.GetContext()
				.GetDefectDiscounts(book.Guid);

			var bookDiscounts = await _dbContextFactory
				.GetContext()
				.GetBookDiscounts(book.BookInfoId);

			var genreDiscounts = await _dbContextFactory
				.GetContext()
				.GetGenreDiscounts(book.BookInfo.Genres);

			discounts.AddRange(defectDiscounts);
			discounts.AddRange(bookDiscounts);
			discounts.AddRange(genreDiscounts);

			return discounts;
		}

		public async Task<BookShopState> GetBookShopState()
		{
			var bookShopState = await _dbContextFactory
				.GetContext()
				.GetOrCreateBookShopState(_bookShopStateId);

			return bookShopState;
		}

		public async Task<List<GetDiscountResponse>> GetDiscountsResponses()
		{
			var discounts = await _dbContextFactory
				.GetContext()
				.GetDiscounts();

			return await MapDiscountsToListGetDiscountResponses(discounts);
		}

		public async Task<GetDiscountResponse> GetDiscountResponse(int discountId)
		{
			return await MapDiscountToGetDiscountResponse(discountId);
		}

		private async Task<List<GetDiscountResponse>> MapDiscountsToListGetDiscountResponses(List<Discount> discounts)
		{
			var result = new List<GetDiscountResponse>();
			foreach (var discount in discounts)
			{
				var discountResponse = await MapDiscountToGetDiscountResponse(discount);
				result.Add(discountResponse);
			}

			return result;
		}
		public async Task DeleteDiscount(int discountId)
		{
			await _dbContextFactory
				.GetContext()
				.DeleteDiscount(discountId);
		}

		public async Task<List<GetDiscountResponse>> GetDiscountResponsesForBook(Guid bookGuid)
		{
			var book = await _dbContextFactory
				.GetContext()
				.GetBook(bookGuid);

			var discounts = await GetDiscounts(book);

			return await MapDiscountsToListGetDiscountResponses(discounts);
		}

		private async Task<GetDiscountResponse> MapDiscountToGetDiscountResponse(Discount discount)
		{
			return await MapDiscountToGetDiscountResponse(discount.Id);
		}
		private async Task<GetDiscountResponse> MapDiscountToGetDiscountResponse(int discountId)
		{
			var discount = await _dbContextFactory
				.GetContext()
				.GetDiscount(discountId);

			if (discount == null)
			{
				return null;
			}

			GetDiscountResponseBuilder builder;

			if (discount is DefectDiscount defectDiscount)
			{
				var defectBuilder = new GetDefectDiscountResponseBuilder();
				builder = defectBuilder;

				defectBuilder.SetBookGuid(defectDiscount.BookGuid);
			}
			else if (discount is BookDiscount bookDiscount)
			{
				var bookBuilder = new GetBookDiscountResponseBuilder();
				builder = bookBuilder;

				bookBuilder.SetBookInfoId(bookDiscount.BookInfoId);
			}
			else if (discount is GenreDiscount genreDiscount)
			{
				var genreBuilder = new GetGenreDiscountResponseBuilder();
				builder = genreBuilder;

				var genre = await _dbContextFactory
					.GetContext()
					.GetGenre(genreDiscount.GenreId);
				if (genre != null)
				{
					genreBuilder.SetGenreName(genre.Name);
				}
			}
			else
			{
				return null;
			}

			builder
				.SetId(discount.Id)
				.SetDiscountPercentage(discount.DiscountPercentage)
				.SetExpireDate(discount.ExpireDate);

			return builder.GetDiscountResponse();
		}

		public async Task AddDiscount(AddDiscountRequest addDiscountRequest)
		{
			if (addDiscountRequest is AddDefectDiscountRequest addDefectDiscountRequest)
			{
				await _dbContextFactory
					.GetContext()
					.CreateDefectDiscount(addDefectDiscountRequest.ExpireDate,
						addDefectDiscountRequest.DiscountPercentage,
						addDefectDiscountRequest.BookGuid);

			}
			else if (addDiscountRequest is AddBookDiscountRequest addBookDiscountRequest)
			{
				await _dbContextFactory
					.GetContext()
					.CreateBookDiscount(addBookDiscountRequest.ExpireDate,
						addBookDiscountRequest.DiscountPercentage,
						addBookDiscountRequest.BookInfoId);
			}
			else if (addDiscountRequest is AddGenreDiscountRequest addGenreDiscountRequest)
			{
				var genre = await _dbContextFactory
					.GetContext()
					.GetOrCreateGenre(addGenreDiscountRequest.GenreName);

				await _dbContextFactory
					.GetContext()
					.CreateGenreDiscount(addGenreDiscountRequest.ExpireDate,
						addGenreDiscountRequest.DiscountPercentage,
						genre.Id);
			}

		}

		public async Task IncreaseMoney(ChangeMoneyBookShopRequest changeMoneyRequest)
		{
			if (changeMoneyRequest.Money <= 0) return;

			await ChangeBookShopMoney(changeMoneyRequest.Money);
		}

		public async Task DecreaseMoney(ChangeMoneyBookShopRequest changeMoneyRequest)
		{
			if (changeMoneyRequest.Money <= 0) return;

			await ChangeBookShopMoney(-changeMoneyRequest.Money);
		}

		private async Task ChangeBookShopMoney(decimal money)
		{
			var bookShopState = await GetBookShopState();
			bookShopState.Balance += money;
			if (bookShopState.Balance < 0)
			{
				bookShopState.Balance = 0;
			}

			await _dbContextFactory
				.GetContext()
				.UpdateBookShopState(bookShopState);
		}


		public async Task IncreaseStorage(ChangeStorageBookShopRequest changeStorageRequest)
		{
			if (changeStorageRequest.AdditionalPlaces <= 0) return;

			await ChangeBookShopStorage(changeStorageRequest.AdditionalPlaces);
		}

		public async Task DecreaseStorage(ChangeStorageBookShopRequest changeStorageRequest)
		{
			if (changeStorageRequest.AdditionalPlaces <= 0) return;

			await ChangeBookShopStorage(-changeStorageRequest.AdditionalPlaces);
		}

		private async Task ChangeBookShopStorage(int size)
		{

			var bookShopState = await GetBookShopState();
			bookShopState.StorageSize += size;

			var booksCount = await _dbContextFactory
				.GetContext()
				.GetBooksCount();

			if (bookShopState.StorageSize < booksCount)
			{
				bookShopState.StorageSize = booksCount;
			}

			await _dbContextFactory
				.GetContext()
				.UpdateBookShopState(bookShopState);
		}


		public async Task<GetBookShopResponse> GetBookShopResponse()
		{
			var bookShopState = await GetBookShopState();

			var booksCount = await _dbContextFactory
				.GetContext()
				.GetBooksCount();

			return new GetBookShopResponse()
			{
				Balance = bookShopState.Balance,
				CurrentBookCount = booksCount,
				StorageSize = bookShopState.StorageSize
			};
		}

		public async Task<bool> NeedMoreBooks()
		{
			var booksCount = await _dbContextFactory
				.GetContext()
				.GetBooksCount();

			var getOldBooksCount = await _dbContextFactory
				.GetContext()
				.GetBooksCount(DateTime.Now.AddMonths(-1));

			var bookShopState = await GetBookShopState();

			return (booksCount < bookShopState.StorageSize * 0.1M) // Have less than 10% books of storage
			       || (getOldBooksCount >= bookShopState.StorageSize * 0.75M); // Old books more than 75% of storage
		}

		public async Task<int> GetBooksOrderCount()
		{
			var bookShopState = await GetBookShopState();

			var booksCount = await _dbContextFactory
				.GetContext()
				.GetBooksCount();

			var needBooks = (int) (bookShopState.StorageSize * 0.1M);
			var leftBookStorageSize = bookShopState.StorageSize - booksCount;

			return (leftBookStorageSize > needBooks) ? needBooks : leftBookStorageSize;
		}
	}
}
