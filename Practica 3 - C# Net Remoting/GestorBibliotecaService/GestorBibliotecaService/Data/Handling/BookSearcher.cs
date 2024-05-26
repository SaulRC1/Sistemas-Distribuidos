using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService.Data.Handling
{
    public class BookSearcher : Searcher<TLibro>
    {
        public const string ISBN_SEARCH_FIELD = "i";
        public const string TITLE_SEARCH_FIELD = "t";
        public const string AUTHOR_SEARCH_FIELD = "a";
        public const string COUNTRY_SEARCH_FIELD = "p";
        public const string LANGUAGE_SEARCH_FIELD = "d";
        public const string ALL_SEARCH_FIELD = "*";

        private string searchField;
        private string searchText;

        public BookSearcher(string searchField, string searchText)
        {
            this.searchField = searchField;
            this.searchText = searchText;
        }

        public List<TLibro> Search(List<TLibro> searched)
        {
            List<TLibro> foundBooks = new List<TLibro>();

            searchField = searchField.ToLowerInvariant();

            if (searchField == ISBN_SEARCH_FIELD)
            {
                foreach (TLibro book in searched)
                {
                    if (book.Isbn.Contains(searchText))
                    {
                        foundBooks.Add(book);
                    }
                }
            }
            else if (searchField == TITLE_SEARCH_FIELD)
            {
                foreach (TLibro book in searched)
                {
                    if (book.Titulo.Contains(searchText))
                    {
                        foundBooks.Add(book);
                    }
                }
            }
            else if (searchField == AUTHOR_SEARCH_FIELD)
            {
                foreach (TLibro book in searched)
                {
                    if (book.Autor.Contains(searchText))
                    {
                        foundBooks.Add(book);
                    }
                }
            }
            else if (searchField == COUNTRY_SEARCH_FIELD)
            {
                foreach (TLibro book in searched)
                {
                    if (book.Pais.Contains(searchText))
                    {
                        foundBooks.Add(book);
                    }
                }
            }
            else if (searchField == LANGUAGE_SEARCH_FIELD)
            {
                foreach (TLibro book in searched)
                {
                    if (book.Idioma.Contains(searchText))
                    {
                        foundBooks.Add(book);
                    }
                }
            }
            else if (searchField == ALL_SEARCH_FIELD)
            {
                foreach (TLibro book in searched)
                {
                    if (book.Isbn.Contains(searchText) || book.Titulo.Contains(searchText)
                            || book.Autor.Contains(searchText) || book.Pais.Contains(searchText)
                            || book.Idioma.Contains(searchText))
                    {
                        foundBooks.Add(book);
                    }
                }
            }

            return foundBooks;
        }
    }
}
