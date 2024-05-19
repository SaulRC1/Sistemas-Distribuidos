package gestor.biblioteca.client.menu;

import gestor.biblioteca.client.properties.GestorBibliotecaUserProperties;
import gestor.biblioteca.client.utils.TDatosRepositorioUtils;
import gestor.biblioteca.service.GestorBibliotecaIntf;
import gestor.biblioteca.service.models.TDatosRepositorio;
import gestor.biblioteca.service.models.TLibro;
import gestor.biblioteca.service.models.handling.BookSearcher;
import static gestor.biblioteca.service.models.handling.BookSearcher.ALL_SEARCH_FIELD;
import static gestor.biblioteca.service.models.handling.BookSearcher.AUTHOR_SEARCH_FIELD;
import static gestor.biblioteca.service.models.handling.BookSearcher.COUNTRY_SEARCH_FIELD;
import static gestor.biblioteca.service.models.handling.BookSearcher.ISBN_SEARCH_FIELD;
import static gestor.biblioteca.service.models.handling.BookSearcher.LANGUAGE_SEARCH_FIELD;
import static gestor.biblioteca.service.models.handling.BookSearcher.TITLE_SEARCH_FIELD;
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
public class MainMenu
{

    public static final String MAIN_MENU_TITLE = "GESTOR BIBLIOTECARIO 2.0 (M.PRINCIPAL)";
    public static final String OPTION_0_TITLE = "0.- Salir";
    public static final String OPTION_1_TITLE = "1.- M. Administración";
    public static final String OPTION_2_TITLE = "2.- Consulta de libros";
    public static final String OPTION_3_TITLE = "3.- Préstamo de libros";
    public static final String OPTION_4_TITLE = "4.- Devolución de libros";

    private GestorBibliotecaIntf gestorBiblioteca;
    private Scanner scanner;

    public MainMenu(GestorBibliotecaIntf gestorBiblioteca, Scanner scanner)
    {
        this.gestorBiblioteca = gestorBiblioteca;
        this.scanner = scanner;
    }

    public void showMainMenu()
    {
        System.out.println(MAIN_MENU_TITLE);
        System.out.println("*******************************");
        System.out.println(OPTION_1_TITLE);
        System.out.println(OPTION_2_TITLE);
        System.out.println(OPTION_3_TITLE);
        System.out.println(OPTION_4_TITLE);
        System.out.println(OPTION_0_TITLE);
    }

    public void executeOption0()
    {
        System.exit(0);
    }

    public void executeOption1()
    {
        try
        {
            //Clean buffer
            scanner.nextLine();

            String password = "";

            System.out.println("Por favor inserte la contraseña de administración:");
            password = scanner.nextLine();

            int connectionResult = gestorBiblioteca.Conexion(password);

            switch (connectionResult)
            {
                case -1:
                    System.out.println("ERROR: Ya hay un usuario identificado como administrador");
                    break;
                case -2:
                    System.out.println("ERROR: Contraseña errónea");
                default:
                    GestorBibliotecaUserProperties.getInstance().setAdminId(connectionResult);

                    AdministrationMenu adminMenu = new AdministrationMenu(gestorBiblioteca, scanner);

                    int option = -1;

                    do
                    {
                        adminMenu.show();

                        System.out.println("Selecciona una opción:");
                        option = scanner.nextInt();

                        adminMenu.executeOption(option);

                    } while (option != 0);
            }
        } catch (RemoteException ex)
        {
            System.out.println("Error de conexión: " + ex.getMessage());
        }
    }

    public void executeOption2()
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

            if (!searchCode.equalsIgnoreCase("i") && !searchCode.equalsIgnoreCase("t")
                    && !searchCode.equalsIgnoreCase("a") && !searchCode.equalsIgnoreCase("p")
                    && !searchCode.equalsIgnoreCase("d") && !searchCode.equalsIgnoreCase("*"))
            {
                System.out.println("ERORR: El código introducido no es válido");
            }
        } while (!searchCode.equalsIgnoreCase("i") && !searchCode.equalsIgnoreCase("t")
                && !searchCode.equalsIgnoreCase("a") && !searchCode.equalsIgnoreCase("p")
                && !searchCode.equalsIgnoreCase("d") && !searchCode.equalsIgnoreCase("*"));

        try
        {
            int totalNumberOfBooks = gestorBiblioteca.NLibros(-1);
            
            if (totalNumberOfBooks <= 0)
            {
                System.out.println("ERROR: No hay repositorios cargados en la biblioteca");

                return;
            }

            BookUtils bookUtils = new BookUtils();

            int adminId = GestorBibliotecaUserProperties.getInstance().getAdminId();

            List<TLibro> allBooks = new ArrayList<>();

            for (int i = 0; i < totalNumberOfBooks; i++)
            {
                TLibro book = gestorBiblioteca.Descargar(adminId, -1, i);

                if (book != null)
                {
                    allBooks.add(book);
                }
            }

            boolean headerShow = true;

            if (searchCode.equalsIgnoreCase(ISBN_SEARCH_FIELD))
            {
                for (int i = 0; i < allBooks.size(); i++)
                {
                    TLibro book = allBooks.get(i);

                    if (book.getIsbn().contains(searchText))
                    {
                        bookUtils.Mostrar(i, headerShow, book);
                        headerShow = false;
                    }
                }
            } else if (searchCode.equalsIgnoreCase(TITLE_SEARCH_FIELD))
            {
                for (int i = 0; i < allBooks.size(); i++)
                {
                    TLibro book = allBooks.get(i);

                    if (book.getTitulo().contains(searchText))
                    {
                        bookUtils.Mostrar(i, headerShow, book);
                        headerShow = false;
                    }
                }
            } else if (searchCode.equalsIgnoreCase(AUTHOR_SEARCH_FIELD))
            {
                for (int i = 0; i < allBooks.size(); i++)
                {
                    TLibro book = allBooks.get(i);

                    if (book.getAutor().contains(searchText))
                    {
                        bookUtils.Mostrar(i, headerShow, book);
                        headerShow = false;
                    }
                }
            } else if (searchCode.equalsIgnoreCase(COUNTRY_SEARCH_FIELD))
            {
                for (int i = 0; i < allBooks.size(); i++)
                {
                    TLibro book = allBooks.get(i);

                    if (book.getPais().contains(searchText))
                    {
                        bookUtils.Mostrar(i, headerShow, book);
                        headerShow = false;
                    }
                }
            } else if (searchCode.equalsIgnoreCase(LANGUAGE_SEARCH_FIELD))
            {
                for (int i = 0; i < allBooks.size(); i++)
                {
                    TLibro book = allBooks.get(i);

                    if (book.getIdioma().contains(searchText))
                    {
                        bookUtils.Mostrar(i, headerShow, book);
                        headerShow = false;
                    }
                }
            } else if (searchCode.equalsIgnoreCase(ALL_SEARCH_FIELD))
            {
                for (int i = 0; i < allBooks.size(); i++)
                {
                    TLibro book = allBooks.get(i);

                    if (book.getIsbn().contains(searchText) || book.getTitulo().contains(searchText)
                            || book.getAutor().contains(searchText) || book.getPais().contains(searchText)
                            || book.getIdioma().contains(searchText))
                    {
                        bookUtils.Mostrar(i, headerShow, book);
                        headerShow = false;
                    }
                }
            }

        } catch (RemoteException ex)
        {
            Logger.getLogger(AdministrationMenu.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    public void executeOption3()
    {

    }

    public void executeOption4()
    {

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
            default:
                System.out.println("opción incorrecta");
        }
    }
}
