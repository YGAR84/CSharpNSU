using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Core;
using BookShop.Core.Discounts;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Infrastructure.EntityFramework
{
	public sealed class BookShopContext : DbContext
	{
		public const string DefaultSchemaName = "BookShop";

		public BookShopContext(DbContextOptions options) : base(options)
		{
			//Database.EnsureCreated();
			//Database.Migrate();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
			modelBuilder.HasDefaultSchema(DefaultSchemaName);
		}

		public async Task<List<Book>> GetBooks()
		{
			return await Set<Book>()
				.Include(b => b.BookInfo)
				.ThenInclude(b => b.Genres)
				.ToListAsync();
		}

		public async Task<int> GetBooksCount()
		{
			return await Set<Book>().CountAsync();
		}

		public async Task<int> GetBooksCount(DateTime arriveDate)
		{
			return await Set<Book>().CountAsync(b => b.ArriveDate < arriveDate);
		}

		public async Task<Book> GetBook(Guid bookGuid)
		{
			return await Set<Book>()
				.Include(b => b.BookInfo)
				.ThenInclude(b => b.Genres)
				.FirstOrDefaultAsync(b => b.Guid == bookGuid);
		}

		public async Task<Genre> GetOrCreateGenre(string genreName)
		{
			Genre genre = await Set<Genre>()
				.FirstOrDefaultAsync(g => g.Name == genreName);
			if (genre == null)
			{
				genre = new Genre(genreName);
				await Set<Genre>().AddAsync(genre);

				await SaveChangesAsync();
			}

			return genre;
		}

		public async Task<Genre> GetGenre(int genreId)
		{
			return await Set<Genre>()
				.FirstOrDefaultAsync(g => g.Id == genreId);
		}

		public async Task<BookInfo> GetOrCreateBookInfo(string author, string title, List<Genre> genres)
		{
			BookInfo bookInfo = await Set<BookInfo>()
				.FirstOrDefaultAsync(bi => bi.Author == author && bi.Title == title);
			if (bookInfo == null)
			{
				bookInfo = new BookInfo(title, author, null);
				await Set<BookInfo>().AddAsync(bookInfo);

				await SaveChangesAsync();

				bookInfo = await Set<BookInfo>().FirstAsync(bi => bi.Id == bookInfo.Id);
				bookInfo.Genres = genres;
				Set<BookInfo>().Update(bookInfo);
				await SaveChangesAsync();
			}

			return bookInfo;
		}

		public async Task<Book> AddBook(Book book)
		{
			await Set<Book>().AddAsync(book);
			await SaveChangesAsync();
			return book;
		}

		public async Task<Book> UpdateBook(Book book)
		{
			Set<Book>().Update(book);
			await SaveChangesAsync();
			return book;
		}

		public async Task DeleteBook(Book book)
		{
			Set<Book>().Remove(book);
			await SaveChangesAsync();
		}

		public async Task DeleteBook(Guid bookGuid)
		{
			Book book = await Set<Book>()
				.FirstOrDefaultAsync(b => b.Guid == bookGuid);
			if (book != null)
			{
				await DeleteBook(book);
			}
		}

		public async Task<BookShopState> GetOrCreateBookShopState(int id)
		{
			BookShopState bookShopState = await Set<BookShopState>()
				.FirstOrDefaultAsync(bss => bss.Id == id);
			if (bookShopState == null)
			{
				bookShopState = new BookShopState();
				await Set<BookShopState>()
					.AddAsync(bookShopState);

				await SaveChangesAsync();
			}

			return bookShopState;
		}

		public async Task UpdateBookShopState(BookShopState bookShopState)
		{
			Set<BookShopState>().Update(bookShopState);
			await SaveChangesAsync();
		}

		public async Task<List<DefectDiscount>> GetDefectDiscounts(Guid bookGuid)
		{
			return await Set<DefectDiscount>()
				.Where(dd => dd.BookGuid == bookGuid)
				.ToListAsync();
		}

		public async Task<List<BookDiscount>> GetBookDiscounts(int bookInfoId)
		{
			return await Set<BookDiscount>()
				.Where(bd => bd.BookInfoId ==bookInfoId)
				.ToListAsync();
		}

		public async Task<List<GenreDiscount>> GetGenreDiscounts(List<Genre> genres)
		{
			var genreDiscounts = new List<GenreDiscount>();
			foreach (var genre in genres)
			{
				var currentDiscounts = await Set<GenreDiscount>()
					.Where(gd => gd.GenreId == genre.Id)
					.ToListAsync();

				genreDiscounts.AddRange(currentDiscounts);
			}

			var a = await Set<GenreDiscount>()
				.Where(gd => genres
					.Select(g => g.Id)
					.Contains(gd.Id))
				.ToListAsync();

			Console.WriteLine(string.Join("," , a.Select(g => g.Id).ToArray()));
			Console.WriteLine(string.Join(",", genreDiscounts.Select(g => g.Id).ToArray()));
			return genreDiscounts;
		}

		public async Task<Discount> GetDiscount(int discountId)
		{
			return await Set<Discount>()
				.FirstOrDefaultAsync(d => d.Id == discountId);
		}

		public async Task<List<Discount>> GetDiscounts()
		{
			return await Set<Discount>().ToListAsync();
		}

		public async Task<DefectDiscount> CreateDefectDiscount(DateTime expireDate, decimal discountPercentage, Guid bookIGuid)
		{
			var defectDiscount = new DefectDiscount
			{
				ExpireDate = expireDate,
				DiscountPercentage = discountPercentage,
				BookGuid = bookIGuid
			};

			await Set<DefectDiscount>()
				.AddAsync(defectDiscount);

			await SaveChangesAsync();
			return defectDiscount;
		}

		public async Task<BookDiscount> CreateBookDiscount(DateTime expireDate, decimal discountPercentage, int bookInfoId)
		{
			var bookDiscount = new BookDiscount()
			{
				ExpireDate = expireDate,
				DiscountPercentage = discountPercentage,
				BookInfoId = bookInfoId
			};

			await Set<BookDiscount>()
				.AddAsync(bookDiscount);

			await SaveChangesAsync();
			return bookDiscount;
		}

		public async Task<GenreDiscount> CreateGenreDiscount(DateTime expireDate, decimal discountPercentage, int genreId)
		{
			var genreDiscount = new GenreDiscount()
			{
				ExpireDate = expireDate,
				DiscountPercentage = discountPercentage,
				GenreId = genreId
			};

			await Set<GenreDiscount>()
				.AddAsync(genreDiscount);

			await SaveChangesAsync();
			return genreDiscount;
		}

		public async Task DeleteDiscount(int discountId)
		{
			var discount = await Set<Discount>()
				.FirstOrDefaultAsync(d => d.Id == discountId);

			if (discount != null)
			{
				await DeleteDiscount(discount);
			}
		}

		public async Task DeleteDiscount(Discount discount)
		{
			Set<Discount>().Remove(discount);

			await SaveChangesAsync();
		}

		public async Task<List<BookInfo>> GetBookInfos()
		{
			return await Set<BookInfo>()
				.Include(bi => bi.Books)
				.Include(bi => bi.Genres)
				.ToListAsync();
		}
	}
}
