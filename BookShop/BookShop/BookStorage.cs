﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop
{
	public class BookStorage
	{
		private Dictionary<BookInfo, List<Book>> _booksByBookInfo = new Dictionary<BookInfo, List<Book>>();
		private Dictionary<Guid, Book> _booksByGuid = new Dictionary<Guid, Book>();


		public void AddBook(Book book)
		{
			if (!_booksByBookInfo.ContainsKey(book.BookInfo))
			{
				_booksByBookInfo[book.BookInfo] = new List<Book> { book };
			}
			else
			{
				_booksByBookInfo[book.BookInfo].Add(book);
			}

			_booksByGuid.Add(book.Guid, book);
		}

		public void DeleteBook(Guid guid)
		{
			if (_booksByGuid.TryGetValue(guid, out var book))
			{
				DeleteBook(book);
			}
		}

		public void DeleteBook(Book book)
		{
			if (!_booksByBookInfo.TryGetValue(book.BookInfo, out var books)) return;

			books.Remove(book);
			_booksByGuid.Remove(book.Guid);
		}

		public Book FindByGuidOrDefault(Guid guid)
		{
			return _booksByGuid.TryGetValue(guid, out var book) ? book : null;
		}

	}
}
