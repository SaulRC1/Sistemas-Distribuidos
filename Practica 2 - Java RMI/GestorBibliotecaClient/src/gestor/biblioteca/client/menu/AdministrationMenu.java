package gestor.biblioteca.client.menu;

import gestor.biblioteca.client.properties.GestorBibliotecaUserProperties;
import gestor.biblioteca.client.utils.TDatosRepositorioUtils;
import gestor.biblioteca.service.GestorBibliotecaIntf;
import gestor.biblioteca.service.models.TDatosRepositorio;
import gestor.biblioteca.service.models.TLibro;
import gestor.biblioteca.service.util.BookUtils;
import java.rmi.RemoteException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public class AdministrationMenu
{
    public static final String ADMIN_MENU_TITLE = "GESTOR BIBLIOTECARIO 2.0 (M.ADMINISTRACIÓN)";
    public static final String OPTION_0_TITLE = "0.- Salir";
    public static final String OPTION_1_TITLE = "1.- Cargar Repositorio";
    public static final String OPTION_2_TITLE = "2.- Guardar Repositorio";
    public static final String OPTION_3_TITLE = "3.- Nuevo Libro";
    public static final String OPTION_4_TITLE = "4.- Comprar Libros";
    public static final String OPTION_5_TITLE = "5.- Retirar Libros";
    public static final String OPTION_6_TITLE = "6.- Ordenar Libros";
    public static final String OPTION_7_TITLE = "7.- Buscar Libros";
    public static final String OPTION_8_TITLE = "8.- Listar Libros";
    
    private GestorBibliotecaIntf gestorBiblioteca;
    private Scanner scanner;

    public AdministrationMenu(GestorBibliotecaIntf gestorBiblioteca, Scanner scanner)
    {
        this.gestorBiblioteca = gestorBiblioteca;
        this.scanner = scanner;
    }
    
    public void executeOption(int optionNumber)
    {
        switch (optionNumber)
        {
            case 0:
                executeOption0();
                break;
            case 1:
                executeOption1();
                break;
            case 2:
                executeOption2();
                break;
            case 3:
                executeOption3();
                break;
            case 4:
                executeOption4();
                break;
            case 5:
                executeOption5();
                break;
            case 6:
                executeOption6();
                break;
            case 7:
                executeOption7();
                break;
            case 8:
                executeOption8();
                break;
            default:
                System.out.println("ERROR: Opción inválida.");
        }
    }
    
    public void show()
    {
        System.out.println(ADMIN_MENU_TITLE);
        System.out.println("*******************************");
        System.out.println(OPTION_1_TITLE);
        System.out.println(OPTION_2_TITLE);
        System.out.println(OPTION_3_TITLE);
        System.out.println(OPTION_4_TITLE);
        System.out.println(OPTION_5_TITLE);
        System.out.println(OPTION_6_TITLE);
        System.out.println(OPTION_7_TITLE);
        System.out.println(OPTION_8_TITLE);
        System.out.println(OPTION_0_TITLE);
    }
    
    public void executeOption0()
    {
        try
        {
            gestorBiblioteca.Desconexion(GestorBibliotecaUserProperties.getInstance().getAdminId());
        } catch (RemoteException ex)
        {
            Logger.getLogger(AdministrationMenu.class.getName()).log(Level.SEVERE, ex.getMessage(), ex);
        }
    }
    
    public void executeOption1()
    {
        //Clean scanner buffer
        scanner.nextLine();
        
        String fileName = "";
        
        System.out.println("Introduce el nombre del fichero de datos");
        fileName = scanner.nextLine();
        
        try
        {
            gestorBiblioteca.AbrirRepositorio(GestorBibliotecaUserProperties.getInstance().getAdminId()
                    , fileName);
        } catch (RemoteException ex)
        {
            Logger.getLogger(AdministrationMenu.class.getName()).log(Level.SEVERE, ex.getMessage(), ex);
        }
    }
    
    public void executeOption2()
    {
        
    }
    
    public void executeOption3()
    {
        //clean buffer
        scanner.nextLine();
        
        String isbn;
        String author;
        String title;
        int year;
        String country;
        String language;
        int available;
        
        System.out.println("Introduce el ISBN: ");
        isbn = scanner.nextLine();
        
        System.out.println("Introduce el autor: ");
        author = scanner.nextLine();
        
        System.out.println("Introduce el título: ");
        title = scanner.nextLine();
        
        System.out.println("Introduce el año:");
        year = scanner.nextInt();
        
        //clean buffer
        scanner.nextLine();
        
        System.out.println("Introduce el país:");
        country = scanner.nextLine();
        
        System.out.println("Introduce el idioma: ");
        language = scanner.nextLine();
        
        System.out.println("Introduce el número de libros inicial:");
        available = scanner.nextInt();
        
        //clean buffer
        scanner.nextLine();
        
        TLibro book = new TLibro(title, author, country, language, isbn, year, 
                available, 0, 0);
        
        try
        {
            int numberOfRepositories = gestorBiblioteca.NRepositorios(
                    GestorBibliotecaUserProperties.getInstance().getAdminId());
            
            List<TDatosRepositorio> repositories = new ArrayList<>();
            
            for (int i = 0; i < numberOfRepositories; i++)
            {
                TDatosRepositorio repository = gestorBiblioteca.DatosRepositorio(
                        GestorBibliotecaUserProperties.getInstance().getAdminId(),
                        i);
                
                if(repository != null)
                {
                    repositories.add(repository);
                }
            }
            
            TDatosRepositorioUtils.showRepositoriesList(repositories);
            
            System.out.println("Elige repositorio: ");
            int repositoryPosition = scanner.nextInt();
            
            gestorBiblioteca.NuevoLibro(GestorBibliotecaUserProperties.getInstance().getAdminId(),
                    book, (repositoryPosition - 1));
            
        } catch (RemoteException ex)
        {
            Logger.getLogger(AdministrationMenu.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
    public void executeOption4()
    {
        
    }
    
    public void executeOption5()
    {
        
    }
    
    public void executeOption6()
    {
        
    }
    
    public void executeOption7()
    {
        
    }
    
    public void executeOption8()
    {
        int adminId = GestorBibliotecaUserProperties.getInstance().getAdminId();
        
        BookUtils bookUtils = new BookUtils();
        
        try
        {
            int totalNumberOfBooks = gestorBiblioteca.NLibros(-1);
            
            System.out.println("Total number of books: " + totalNumberOfBooks);
            
            for (int i = 0; i < totalNumberOfBooks; i++)
            {
                TLibro book = gestorBiblioteca.Descargar(adminId, -1, i);
                
                if(i == 0)
                {
                   bookUtils.Mostrar(i, true, book); 
                }
                else
                {
                    bookUtils.Mostrar(i, false, book);
                }  
            }
            
        } catch (RemoteException ex)
        {
            Logger.getLogger(AdministrationMenu.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}
