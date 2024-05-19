package gestor.biblioteca.service.models;

import java.io.Serializable;
import java.util.Objects;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public class TDatosRepositorio implements Serializable
{
    private String repositoryName;
    private String repositoryAddress;
    private int numberOfBooks;
    private String repositoryFilePath;
    
    private BookRepository bookRepository;

    public TDatosRepositorio()
    {
    }

    public TDatosRepositorio(String repositoryName, String repositoryAddress, int numberOfBooks,
            BookRepository bookRepository, String repositoryFilePath)
    {
        this.repositoryName = repositoryName;
        this.repositoryAddress = repositoryAddress;
        this.numberOfBooks = numberOfBooks;
        this.bookRepository = bookRepository;
        this.repositoryFilePath = repositoryFilePath;
    }

    public String getRepositoryName()
    {
        return repositoryName;
    }

    public void setRepositoryName(String repositoryName)
    {
        this.repositoryName = repositoryName;
    }

    public String getRepositoryAddress()
    {
        return repositoryAddress;
    }

    public void setRepositoryAddress(String repositoryAddress)
    {
        this.repositoryAddress = repositoryAddress;
    }

    public int getNumberOfBooks()
    {
        return numberOfBooks;
    }

    public void setNumberOfBooks(int numberOfBooks)
    {
        this.numberOfBooks = numberOfBooks;
    }

    public BookRepository getBookRepository()
    {
        return bookRepository;
    }

    public void setBookRepository(BookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }

    public String getRepositoryFilePath()
    {
        return repositoryFilePath;
    }

    public void setRepositoryFilePath(String repositoryFilePath)
    {
        this.repositoryFilePath = repositoryFilePath;
    }

    @Override
    public int hashCode()
    {
        int hash = 5;
        hash = 89 * hash + Objects.hashCode(this.repositoryName);
        hash = 89 * hash + Objects.hashCode(this.repositoryAddress);
        hash = 89 * hash + this.numberOfBooks;
        return hash;
    }

    @Override
    public boolean equals(Object obj)
    {
        if (this == obj)
        {
            return true;
        }
        if (obj == null)
        {
            return false;
        }
        if (getClass() != obj.getClass())
        {
            return false;
        }
        final TDatosRepositorio other = (TDatosRepositorio) obj;
        if (this.numberOfBooks != other.numberOfBooks)
        {
            return false;
        }
        if (!Objects.equals(this.repositoryName, other.repositoryName))
        {
            return false;
        }
        return Objects.equals(this.repositoryAddress, other.repositoryAddress);
    }
    
}
