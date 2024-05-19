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
            int result = gestorBiblioteca.AbrirRepositorio(GestorBibliotecaUserProperties.getInstance().getAdminId(),
                     fileName);
            
            switch (result)
            {
                case -1:
                    System.out.println("ERROR: Ya hay un usuario identificado como administrador");
                    break;
                case -2:
                    System.out.println("ERROR: Ya existe un repositorio cargado con el mismo nombre de fichero");
                    break;
                case 0:
                    System.out.println("ERROR: No se ha podido abrir el fichero de repositorio.");
                    break;
                case 1:
                    System.out.println("** El repositorio ha sido cargado **");
                    break;
                default:
                    break;
            }
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
            
            if(numberOfRepositories == -1)
            {
                System.out.println("ERROR: Ya hay otro usuario identificado como"
                        + "administrador");
                
                return;
            }
            
            if(numberOfRepositories == 0)
            {
                System.out.println("ERROR: No hay repositorios cargados en la biblioteca");
                return;
            }

            List<TDatosRepositorio> repositories = new ArrayList<>();

            for (int i = 0; i < numberOfRepositories; i++)
            {
                TDatosRepositorio repository = gestorBiblioteca.DatosRepositorio(
                        GestorBibliotecaUserProperties.getInstance().getAdminId(),
                        i);

                if (repository != null)
                {
                    repositories.add(repository);
                }
            }

            TDatosRepositorioUtils.showRepositoriesList(repositories);

            System.out.println("Elige repositorio: ");
            int repositoryPosition = scanner.nextInt();

            int result = gestorBiblioteca.NuevoLibro(GestorBibliotecaUserProperties.getInstance().getAdminId(),
                    book, (repositoryPosition - 1));
            
            switch (result)
            {
                case -1:
                    System.out.println("ERROR: Ya hay un usuario identificado como "
                            + "administrador");
                    break;
                case -2:
                    System.out.println("ERROR: El repositorio indicado no existe");
                    break;
                case 0:
                    System.out.println("ERROR: El libro ya existe dentro de la biblioteca");
                    break;
                case 1:
                    System.out.println("** El libro ha sido añadido correctamente **");
                    break;
                default:
                    break;
            }

        } catch (RemoteException ex)
        {
            System.out.println("ERROR: " + ex.getMessage());
        }
    }

    public void executeOption4()
    {
        //Clean buffer
        scanner.nextLine();
        
        String isbn = "";
        
        System.out.println("Introduce ISBN a buscar: ");
        isbn = scanner.nextLine();
        
        try
        {
            int result = gestorBiblioteca.Buscar(GestorBibliotecaUserProperties.getInstance()
                    .getAdminId(), isbn);
            
            switch (result)
            {
                case -2:
                    System.out.println("ERROR: Ya hay un usuario identificado como"
                            + "administrador");
                    break;
                case -1:
                    System.out.println("ERROR: No se ha encontrado ningún libro con"
                            + " el ISBN indicado");
                    break;
                default:
                    TLibro book = gestorBiblioteca.Descargar(GestorBibliotecaUserProperties.getInstance()
                            .getAdminId(), -1, result);
                    
                    if(book == null)
                    {
                        System.out.println("ERROR: El libro no existe");
                        return;
                    }   
                    
                    BookUtils bookUtils = new BookUtils();
                    bookUtils.Mostrar(result, true, book);
                    
                    String option = "";
                    
                    do
                    {
                        System.out.println("¿Es este el libro del que deseas comprar"
                            + " más unidades? (s/n)");
                        option = scanner.nextLine();
                        
                        if(!option.equalsIgnoreCase("s") && !option.equalsIgnoreCase("n"))
                        {
                            System.out.println("ERROR: Opción incorrecta, indique"
                                    + "sí o no");
                        }
                    } while (!option.equalsIgnoreCase("s") && !option.equalsIgnoreCase("n"));
                    
                    if(option.equalsIgnoreCase("s"))
                    {
                        int boughtBooks = 0;
                        
                        System.out.println("Introduce el número de libros comprados:");
                        boughtBooks = scanner.nextInt();
                        
                        int buyResult = gestorBiblioteca.Comprar(GestorBibliotecaUserProperties.getInstance()
                            .getAdminId(), isbn, boughtBooks);
                        
                        if(buyResult == -1)
                        {
                            System.out.println("ERROR: Ya hay un usuario identificado como"
                            + "administrador");
                        }
                        else if(buyResult == 0)
                        {
                            System.out.println("ERROR: No se ha encontrado ningún libro con"
                            + " el ISBN indicado");
                        }
                        else if(buyResult == 1)
                        {
                            System.out.println("** Se han añadido los nuevos libros **");
                        }
                    }
                    
                    break;
            }
        } catch (RemoteException ex)
        {
            System.out.println("ERROR: " + ex.getMessage());
        }
    }

    public void executeOption5()
    {
        //Clean buffer
        scanner.nextLine();
        
        String isbn = "";
        
        System.out.println("Introduce ISBN a buscar: ");
        isbn = scanner.nextLine();
        
        try
        {
            int result = gestorBiblioteca.Buscar(GestorBibliotecaUserProperties.getInstance()
                    .getAdminId(), isbn);
            
            switch (result)
            {
                case -2:
                    System.out.println("ERROR: Ya hay un usuario identificado como"
                            + "administrador");
                    break;
                case -1:
                    System.out.println("ERROR: No se ha encontrado ningún libro con"
                            + " el ISBN indicado");
                    break;
                default:
                    TLibro book = gestorBiblioteca.Descargar(GestorBibliotecaUserProperties.getInstance()
                            .getAdminId(), -1, result);
                    
                    if(book == null)
                    {
                        System.out.println("ERROR: El libro no existe");
                        return;
                    }   
                    
                    BookUtils bookUtils = new BookUtils();
                    bookUtils.Mostrar(result, true, book);
                    
                    String option = "";
                    
                    do
                    {
                        System.out.println("¿Es este el libro del que deseas retirar"
                            + " unidades? (s/n)");
                        option = scanner.nextLine();
                        
                        if(!option.equalsIgnoreCase("s") && !option.equalsIgnoreCase("n"))
                        {
                            System.out.println("ERROR: Opción incorrecta, indique"
                                    + "sí o no");
                        }
                    } while (!option.equalsIgnoreCase("s") && !option.equalsIgnoreCase("n"));
                    
                    if(option.equalsIgnoreCase("s"))
                    {
                        int removedBooks = 0;
                        
                        System.out.println("Introduce el número de libros a retirar:");
                        removedBooks = scanner.nextInt();
                        
                        int removeResult = gestorBiblioteca.Retirar(GestorBibliotecaUserProperties.getInstance()
                            .getAdminId(), isbn, removedBooks);
                        
                        if(removeResult == -1)
                        {
                            System.out.println("ERROR: Ya hay un usuario identificado como"
                            + "administrador");
                        }
                        else if(removeResult == 0)
                        {
                            System.out.println("ERROR: No se ha encontrado ningún libro con"
                            + " el ISBN indicado");
                        }
                        else if(removeResult == 1)
                        {
                            System.out.println("** Se han retirado el número de libros indicados **");
                        }
                        else if(removeResult == 2)
                        {
                            System.out.println("ERROR: El número de libros retirados excede los disponibles");
                        }
                    }
                    
                    break;
            }
        } catch (RemoteException ex)
        {
            System.out.println("ERROR: " + ex.getMessage());
        }
    }

    public void executeOption6()
    {
        //Clean buffer
        scanner.nextLine();

        int sortingCode = -1;

        System.out.println("Código de Ordenación");
        System.out.println("0.- Por ISBN");
        System.out.println("1.- Por Título");
        System.out.println("2.- Por Autor");
        System.out.println("3.- Por Año");
        System.out.println("4.- Por País");
        System.out.println("5.- Por Idioma");
        System.out.println("6.- Por nº de libros disponibles");
        System.out.println("7.- Por nº de libros prestados");
        System.out.println("8.- Por nº de libros en espera");

        System.out.println("Introduce código:");
        sortingCode = scanner.nextInt();

        try
        {
            boolean result = gestorBiblioteca.Ordenar(GestorBibliotecaUserProperties
                    .getInstance().getAdminId(), sortingCode);
            
            if(result == true)
            {
                System.out.println("** La biblioteca ha sido ordenada correctamente **");
            }
            else
            {
                System.out.println("ERROR: No se ha podido ordenar la biblioteca");
            }

        } catch (RemoteException ex)
        {
            System.out.println("ERROR: " + ex.getMessage());
        }

    }

    public void executeOption7()
    {
        //Clean buffer
        scanner.nextLine();

        String searchText = "";

        System.out.println("Introduce el texto a buscar: ");
        searchText = scanner.nextLine();

        String searchCode = "";

        System.out.println("Código de Búsqueda");
        System.out.println("I.- Por ISBN");
        System.out.println("T.- Por Título");
        System.out.println("A.- Por Autor");
        System.out.println("P.- Por País");
        System.out.println("D.- Por Idioma");
        System.out.println("*.- Por todos los campos");

        do
        {
            System.out.println("Introduce código: ");
            searchCode = scanner.nextLine();

            if (!searchCode.equalsIgnoreCase("i") || !searchCode.equalsIgnoreCase("t")
                    || !searchCode.equalsIgnoreCase("a") || !searchCode.equalsIgnoreCase("p")
                    || !searchCode.equalsIgnoreCase("d") || !searchCode.equalsIgnoreCase("*"))
            {
                System.out.println("ERORR: El código introducido no es válido");
            }
        } while (!searchCode.equalsIgnoreCase("i") || !searchCode.equalsIgnoreCase("t")
                || !searchCode.equalsIgnoreCase("a") || !searchCode.equalsIgnoreCase("p")
                || !searchCode.equalsIgnoreCase("d") || !searchCode.equalsIgnoreCase("*"));

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

                if (repository != null)
                {
                    repositories.add(repository);
                }
            }

            TDatosRepositorioUtils.showRepositoriesList(repositories);

            System.out.println("Elige repositorio: ");
            int repositoryPosition = scanner.nextInt();

        } catch (RemoteException ex)
        {
            Logger.getLogger(AdministrationMenu.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    public void executeOption8()
    {
        int adminId = GestorBibliotecaUserProperties.getInstance().getAdminId();

        BookUtils bookUtils = new BookUtils();

        try
        {
            int totalNumberOfBooks = gestorBiblioteca.NLibros(-1);

            System.out.println("Número total de libros: " + totalNumberOfBooks);

            for (int i = 0; i < totalNumberOfBooks; i++)
            {
                TLibro book = gestorBiblioteca.Descargar(adminId, -1, i);

                if (i == 0)
                {
                    bookUtils.Mostrar(i, true, book);
                } else
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
