using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService.Data
{
    public interface BookRepository
    {
        void AddBook(TLibro book);

        void DeleteBook(TLibro book);

        TLibro GetBookByISBN(String isbn);

        int GetNumberOfBooks();

        List<TLibro> GetAllBooks();

        //void SortBooks(Comparator<TLibro> comparator);

        //List<TLibro> GetBooksBy(Searcher<TLibro> searcher);
    }
}
