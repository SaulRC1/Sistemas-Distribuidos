package gestor.biblioteca.server.remote;

import gestor.biblioteca.server.data.handling.BookComparator;
import gestor.biblioteca.service.GestorBibliotecaIntf;
import gestor.biblioteca.service.models.BookRepository;
import gestor.biblioteca.service.models.InMemoryBookRepository;
import gestor.biblioteca.service.models.TDatosRepositorio;
import gestor.biblioteca.service.models.TLibro;
import java.io.DataInputStream;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.nio.file.Paths;
import java.rmi.RemoteException;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public class GestorBiblioteca implements GestorBibliotecaIntf
{

    private static final String adminPassword = "1234";

    private int adminId = -1;

    private List<TDatosRepositorio> loadedRepositories = new ArrayList<>();

    private List<TLibro> generalBookStorage = new ArrayList<>();
    
    //By default it will be ISBN
    private int sortingField = BookComparator.ISBN_SORTING_FIELD;

    @Override
    public int Conexion(String pPasswd) throws RemoteException
    {
        if (adminId >= 1)
        {
            //There is and admin identified already
            return -1;
        }

        if (!pPasswd.equals(adminPassword))
        {
            //Wrong password
            return -2;
        }

        adminId = (int) (Math.random() * (1000000 - 1 + 1) + 1);

        return adminId;
    }

    @Override
    public boolean Desconexion(int pIda) throws RemoteException
    {
        if (pIda != adminId)
        {
            return false;
        }

        adminId = -1;

        return true;
    }

    @Override
    public int NRepositorios(int pIda) throws RemoteException
    {
        if (pIda != adminId)
        {
            return -1;
        }

        return this.loadedRepositories.size();
    }

    @Override
    public TDatosRepositorio DatosRepositorio(int pIda, int pPosRepo) throws RemoteException
    {
        if (pIda != adminId)
        {
            return null;
        }

        if (pPosRepo >= this.loadedRepositories.size())
        {
            return null;
        }

        return this.loadedRepositories.get(pPosRepo);
    }

    @Override
    public int AbrirRepositorio(int pIda, String pNomFichero) throws RemoteException
    {
        if (pIda != adminId)
        {
            return -1;
        }

        try
        {
            DataInputStream inputStream = new DataInputStream(new FileInputStream(
                    Paths.get(pNomFichero).toAbsolutePath().toString()));

            int numberOfBooks = inputStream.readInt();
            String repositoryName = inputStream.readUTF();
            String repositoryAddress = inputStream.readUTF();

            BookRepository bookRepository = new InMemoryBookRepository();

            TDatosRepositorio repositoryData = new TDatosRepositorio(repositoryName,
                    repositoryAddress, numberOfBooks, bookRepository);

            if (loadedRepositories.contains(repositoryData))
            {
                return -2;
            }

            for (int i = 0; i < numberOfBooks; i++)
            {
                String isbn = inputStream.readUTF();
                String title = inputStream.readUTF();
                String author = inputStream.readUTF();
                int year = inputStream.readInt();
                String country = inputStream.readUTF();
                String language = inputStream.readUTF();
                int available = inputStream.readInt();
                int borrowed = inputStream.readInt();
                int booked = inputStream.readInt();

                TLibro book = new TLibro(title, author, country, language, isbn, year, available, borrowed, booked);

                repositoryData.getBookRepository().addBook(book);
                generalBookStorage.add(book);
            }

            loadedRepositories.add(repositoryData);
            
            Ordenar(pIda, sortingField);

        } catch (FileNotFoundException ex)
        {
            Logger.getLogger(GestorBiblioteca.class.getName()).log(Level.SEVERE, null, ex);
            return 0;
        } catch (IOException ex)
        {
            Logger.getLogger(GestorBiblioteca.class.getName()).log(Level.SEVERE, null, ex);
            return 0;
        }

        return 1;
    }

    @Override
    public int GuardarRepositorio(int pIda, int pRepo) throws RemoteException
    {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public int NuevoLibro(int pIda, TLibro L, int pRepo) throws RemoteException
    {
        if (pIda != adminId)
        {
            return -1;
        }

        if (pRepo < 0 || pRepo >= this.loadedRepositories.size())
        {
            return -2;
        }

        if (Buscar(pIda, L.getIsbn()) >= 0)
        {
            return 0;
        }

        TDatosRepositorio repository = this.loadedRepositories.get(pRepo);

        repository.getBookRepository().addBook(L);
        repository.setNumberOfBooks(repository.getNumberOfBooks() + 1);
        generalBookStorage.add(L);
        
        Ordenar(pIda, sortingField);

        return 1;
    }

    @Override
    public int Comprar(int pIda, String pIsbn, int pNoLibros) throws RemoteException
    {
        if(pIda != adminId)
        {
            return -1;
        }
        
        int bookPosition = Buscar(pIda, pIsbn);
        
        if (bookPosition < 0)
        {
            return 0;
        }
        
        TLibro book = this.generalBookStorage.get(bookPosition);
        
        book.setDisponibles(book.getDisponibles() + pNoLibros);
        
        if(book.getDisponibles() >= book.getReservados())
        {
            book.setDisponibles(book.getDisponibles() - book.getReservados());
            book.setPrestados(book.getPrestados() + book.getReservados());
            book.setReservados(0);  
        }
        else
        {
            book.setReservados(book.getReservados() - book.getDisponibles());
            book.setPrestados(book.getPrestados() + book.getDisponibles());
            book.setDisponibles(0);
        }
        
        Ordenar(pIda, sortingField);
        
        return 1;
    }

    @Override
    public int Retirar(int pIda, String pIsbn, int pNoLibros) throws RemoteException
    {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public boolean Ordenar(int pIda, int pCampo) throws RemoteException
    {
        if(pIda != adminId)
        {
            return false;
        }
        
        if(pCampo < BookComparator.ISBN_SORTING_FIELD || pCampo > BookComparator.BOOKED_SORTING_FIELD)
        {
            return false;
        }
        
        this.sortingField = pCampo;
        
        this.generalBookStorage.sort(new BookComparator(sortingField));
        
        for (TDatosRepositorio repository : loadedRepositories)
        {
            repository.getBookRepository().sortBooks(new BookComparator(sortingField));
        }
        
        return true;
    }

    @Override
    public int NLibros(int pRepo) throws RemoteException
    {
        if (pRepo == -1)
        {
            return generalBookStorage.size();
        }

        if (pRepo >= this.loadedRepositories.size())
        {
            return -1;
        }

        return this.loadedRepositories.get(pRepo).getBookRepository().getNumberOfBooks();
    }

    @Override
    public int Buscar(int pIda, String pIsbn) throws RemoteException
    {
        if (pIda != adminId)
        {
            return -2;
        }

        for (int i = 0; i < generalBookStorage.size(); i++)
        {
            TLibro book = generalBookStorage.get(i);

            if (pIsbn.equals(book.getIsbn()))
            {
                return i;
            }
        }

        return -1;
    }

    @Override
    public TLibro Descargar(int pIda, int pRepo, int pPos) throws RemoteException
    {
        if (pRepo == -1)
        {
            if (pPos >= this.generalBookStorage.size())
            {
                return null;
            }

            TLibro book = null;

            try
            {
                book = (TLibro) this.generalBookStorage.get(pPos).clone();

                if (pIda != adminId)
                {
                    book.setReservados(0);
                    book.setPrestados(0);
                }
            } catch (CloneNotSupportedException ex)
            {
                Logger.getLogger(GestorBiblioteca.class.getName()).log(Level.SEVERE, ex.getMessage(), ex);
            }

            return book;
        }

        if (pPos >= this.loadedRepositories.get(pRepo).getBookRepository()
                .getNumberOfBooks())
        {
            return null;
        }

        TDatosRepositorio repository = this.loadedRepositories.get(pRepo);

        List<TLibro> repositoryBooks = repository.getBookRepository().getAllBooks();

        TLibro book = null;

        if (pPos < 0 || pPos >= repositoryBooks.size())
        {
            return null;
        }

        try
        {
            book = (TLibro) repositoryBooks.get(pPos).clone();

            if (pIda != adminId)
            {
                book.setReservados(0);
                book.setPrestados(0);
            }
        } catch (CloneNotSupportedException ex)
        {
            Logger.getLogger(GestorBiblioteca.class.getName()).log(Level.SEVERE, null, ex);
        }

        return book;
    }

    @Override
    public int Prestar(int pPos) throws RemoteException
    {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public int Devolver(int pPos) throws RemoteException
    {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

}
