package gestor.biblioteca.service.models;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public interface BookRepository
{

    public void addBook(TLibro book);

    public void deleteBook(TLibro book);

    public TLibro getBookByISBN(String isbn);
    
}
