package gestor.biblioteca.server.remote;

import gestor.biblioteca.service.GestorBibliotecaIntf;
import gestor.biblioteca.service.models.BookRepository;
import gestor.biblioteca.service.models.InMemoryBookRepository;
import gestor.biblioteca.service.models.TDatosRepositorio;
import gestor.biblioteca.service.models.TLibro;
import java.io.BufferedInputStream;
import java.io.DataInputStream;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;
import java.math.BigInteger;
import java.nio.file.Paths;
import java.rmi.RemoteException;
import java.util.ArrayList;
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

    @Override
    public int Conexion(String pPasswd) throws RemoteException
    {
        if(adminId >= 1)
        {
            //There is and admin identified already
            return -1;
        }
        
        if(!pPasswd.equals(adminPassword))
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
        if(pIda != adminId)
        {
            return false;
        }
        
        adminId = -1;
        
        return true;
    }

    @Override
    public int NRepositorios(int pIda) throws RemoteException
    {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public TDatosRepositorio DatosRepositorio(int pIda, int pPosRepo) throws RemoteException
    {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public int AbrirRepositorio(int pIda, String pNomFichero) throws RemoteException
    {
        if(pIda != adminId)
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
            
            if(loadedRepositories.contains(repositoryData))
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
            }
            
            loadedRepositories.add(repositoryData);
            
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
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public int Comprar(int pIda, String pIsbn, int pNoLibros) throws RemoteException
    {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public int Retirar(int pIda, String pIsbn, int pNoLibros) throws RemoteException
    {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public boolean Ordenar(int pIda, int pCampo) throws RemoteException
    {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public int NLibros(int pRepo) throws RemoteException
    {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public int Buscar(int pIda, String pIsbn) throws RemoteException
    {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public TLibro Descargar(int pIda, int pRepo, int pPos) throws RemoteException
    {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
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
