package gestor.biblioteca.service.models;

import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public class InMemoryBookRepository implements BookRepository
{
    private List<TLibro> books = new ArrayList<>();

    @Override
    public void addBook(TLibro book)
    {
        books.add(book);
    }

    @Override
    public void deleteBook(TLibro book)
    {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public TLibro getBookByISBN(String isbn)
    {
        for (TLibro book : books)
        {
            if(book.getIsbn().equals(isbn))
            {
                return book;
            }
        }
        
        return null;
    }

}
