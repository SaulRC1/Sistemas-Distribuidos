package gestor.biblioteca.service.models;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public class InMemoryBookRepository implements BookRepository, Serializable
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

    @Override
    public int getNumberOfBooks()
    {
        return books.size();
    }

    @Override
    public List<TLibro> getAllBooks()
    {
        return this.books;
    }

    @Override
    public void sortBooks(Comparator<TLibro> comparator)
    {
        this.books.sort(comparator);
    }

}
