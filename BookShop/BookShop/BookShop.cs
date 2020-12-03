using System;
using System.Collections.Generic;
using BookShop.Core.Discounts;

namespace BookShop.Core
{
	public class BookShop
	{
		private decimal _balance;
		private BookStorage _bookStorage;
		private DateTime _currentDate;

		public decimal Balance => _balance;
		public BookStorage BookStorage => _bookStorage;
		public DateTime CurrentDate => _currentDate;

		private readonly List<Discount> _discounts = new List<Discount>();
		public IReadOnlyList<Discount> Discounts => _discounts;


		public BookShop(BookStorage bookStorage, DateTime startDate, decimal balance = 0)
		{
			_balance = balance;
			_currentDate = startDate;
			_bookStorage = bookStorage;
		}

		public BookShop(DateTime startDate, decimal balance = 0) : this(new BookStorage(), startDate, balance) { }

		public void AddBook(Book book)
		{
			_bookStorage.AddBook(book);
		}

		public void AddDiscount(Discount discount)
		{
			_discounts.Add(discount);
		}

		public void AddDays(int days)
		{
			_currentDate = _currentDate.AddDays(days);
		}

		public void SellBook(Guid bookGuid)
		{
			_balance += GetCost(bookGuid);
			_bookStorage.DeleteBook(bookGuid);
		}

		public decimal GetCost(Guid bookGuid)
		{
			var book = _bookStorage.FindByGuidOrDefault(bookGuid);
			if (book == null)
			{
				return 0m;
			}

			return GetCost(book);
		}

		public decimal GetCost(Book book)
		{
			foreach (var discount in _discounts)
			{
				book = discount.ApplyDiscount(book, _currentDate);
			}
			return book.Cost;
		}

	}

}