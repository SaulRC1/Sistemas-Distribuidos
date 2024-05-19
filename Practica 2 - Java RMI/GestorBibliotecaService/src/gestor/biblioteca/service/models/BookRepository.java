package gestor.biblioteca.service.models;

import gestor.biblioteca.service.models.handling.Searcher;
import java.util.Comparator;
import java.util.List;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public interface BookRepository
{

    public void addBook(TLibro book);

    public void deleteBook(TLibro book);

    public TLibro getBookByISBN(String isbn);
    
    public int getNumberOfBooks();
    
    public List<TLibro> getAllBooks();
    
    public void sortBooks(Comparator<TLibro> comparator);
    
    public List<TLibro> getBooksBy(Searcher<TLibro> searcher);
    
}
