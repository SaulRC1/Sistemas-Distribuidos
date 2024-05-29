using GestorBibliotecaService.Data.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService.Data
{
    public class InMemoryBookRepository : BookRepository
    {
        private List<TLibro> books = new List<TLibro>();

        public void AddBook(TLibro book)
        {
            if (!books.Contains(book))
            {
                books.Add(book);
            }
        }

        public void DeleteBook(TLibro book)
        {
            books.Remove(book);
        }

        public List<TLibro> GetAllBooks()
        {
            return this.books;
        }

        public TLibro GetBookByISBN(string isbn)
        {
            foreach (TLibro book in books)
            {
                if (book.Isbn == isbn)
                {
                    return book;
                }
            }

            return null;
        }

        public List<TLibro> GetBooksBy(Searcher<TLibro> searcher)
        {
            return searcher.Search(this.books);
        }

        public int GetNumberOfBooks()
        {
            return this.books.Count;
        }

        public void SortBooks(IComparer<TLibro> comparer)
        {
            this.books.Sort(comparer);
        }
    }
}
