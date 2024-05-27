using GestorBibliotecaService.Data.Handling;
using GestorBibliotecaService.UserProperties;
using GestorBibliotecaService.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService.Menu
{
    public class AdministrationMenu
    {
        public const string ADMIN_MENU_TITLE = "GESTOR BIBLIOTECARIO 2.0 (M.ADMINISTRACIÓN)";
        public const string OPTION_0_TITLE = "0.- Salir";
        public const string OPTION_1_TITLE = "1.- Cargar Repositorio";
        public const string OPTION_2_TITLE = "2.- Guardar Repositorio";
        public const string OPTION_3_TITLE = "3.- Nuevo Libro";
        public const string OPTION_4_TITLE = "4.- Comprar Libros";
        public const string OPTION_5_TITLE = "5.- Retirar Libros";
        public const string OPTION_6_TITLE = "6.- Ordenar Libros";
        public const string OPTION_7_TITLE = "7.- Buscar Libros";
        public const string OPTION_8_TITLE = "8.- Listar Libros";

        private GestorBibliotecaService gestorBiblioteca;

        public AdministrationMenu(GestorBibliotecaService gestorBiblioteca)
        {
            this.gestorBiblioteca = gestorBiblioteca;
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
                    Console.WriteLine("ERROR: Opción inválida.");
                    break;
            }
        }

        public void show()
        {
            Console.WriteLine(ADMIN_MENU_TITLE);
            Console.WriteLine("*******************************");
            Console.WriteLine(OPTION_1_TITLE);
            Console.WriteLine(OPTION_2_TITLE);
            Console.WriteLine(OPTION_3_TITLE);
            Console.WriteLine(OPTION_4_TITLE);
            Console.WriteLine(OPTION_5_TITLE);
            Console.WriteLine(OPTION_6_TITLE);
            Console.WriteLine(OPTION_7_TITLE);
            Console.WriteLine(OPTION_8_TITLE);
            Console.WriteLine(OPTION_0_TITLE);
        }

        public void executeOption0()
        {
            try
            {
                gestorBiblioteca.Desconexion(GestorBibliotecaUserProperties.getInstance().AdminId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        public void executeOption1()
        {
            String fileName = "";

            Console.WriteLine("Introduce el nombre del fichero de datos");
            fileName = Console.ReadLine();

            try
            {
                int result = gestorBiblioteca.AbrirRepositorio(GestorBibliotecaUserProperties.getInstance().AdminId,
                         fileName);

                switch (result)
                {
                    case -1:
                        Console.WriteLine("ERROR: Ya hay un usuario identificado como administrador");
                        break;
                    case -2:
                        Console.WriteLine("ERROR: Ya existe un repositorio cargado con el mismo nombre de fichero");
                        break;
                    case 0:
                        Console.WriteLine("ERROR: No se ha podido abrir el fichero de repositorio.");
                        break;
                    case 1:
                        Console.WriteLine("** El repositorio ha sido cargado **");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        public void executeOption2()
        {
            try
            {
                int numberOfRepositories = gestorBiblioteca.NRepositorios(
                        GestorBibliotecaUserProperties.getInstance().AdminId);

                if (numberOfRepositories == -1)
                {
                    Console.WriteLine("ERROR: Ya hay otro usuario identificado como"
                            + "administrador");

                    return;
                }

                if (numberOfRepositories == 0)
                {
                    Console.WriteLine("ERROR: No hay repositorios cargados en la biblioteca");
                    return;
                }

                List<TDatosRepositorio> repositories = new List<TDatosRepositorio>();

                for (int i = 0; i < numberOfRepositories; i++)
                {
                    TDatosRepositorio repository = gestorBiblioteca.DatosRepositorio(
                            GestorBibliotecaUserProperties.getInstance().AdminId,
                            i);

                    if (repository != null)
                    {
                        repositories.Add(repository);
                    }
                }

                TDatosRepositorioUtils.showRepositoriesListWithAllOption(repositories);

                int repositoryPosition;
                bool repositoryPositionParsed = false;

                do
                {
                    Console.WriteLine("Elige repositorio: ");
                    repositoryPositionParsed = Int32.TryParse(Console.ReadLine(), out repositoryPosition);

                    if (!repositoryPositionParsed)
                    {
                        Console.WriteLine("Error: introduzca un valor válido");
                    }

                } while (!repositoryPositionParsed);
                

                int result = gestorBiblioteca.GuardarRepositorio(GestorBibliotecaUserProperties
                        .getInstance().AdminId, (repositoryPosition - 1));

                if (result == -1)
                {
                    Console.WriteLine("ERROR: Ya hay un usuario identificado como"
                            + " administrador");
                }
                else if (result == -2)
                {
                    Console.WriteLine("ERROR: El repositorio indicado no existe");
                }
                else if (result == 0)
                {
                    Console.WriteLine("ERROR: No se ha podido guardar el/los repositorio/s");
                }
                else if (result == 1)
                {
                    Console.WriteLine("** Se ha guardado el/los repositorio/s **");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        public void executeOption6()
        {
            int sortingCode = -1;

            Console.WriteLine("Código de Ordenación");
            Console.WriteLine("0.- Por ISBN");
            Console.WriteLine("1.- Por Título");
            Console.WriteLine("2.- Por Autor");
            Console.WriteLine("3.- Por Año");
            Console.WriteLine("4.- Por País");
            Console.WriteLine("5.- Por Idioma");
            Console.WriteLine("6.- Por nº de libros disponibles");
            Console.WriteLine("7.- Por nº de libros prestados");
            Console.WriteLine("8.- Por nº de libros en espera");

            do
            {
                Console.WriteLine("Introduce código:");
                Int32.TryParse(Console.ReadLine(), out sortingCode);

                if (sortingCode < 0 || sortingCode > 8)
                {
                    Console.WriteLine("Error: Introduzca un valor válido");
                }

            } while (sortingCode < 0 || sortingCode > 8);


            try
            {
                bool result = gestorBiblioteca.Ordenar(GestorBibliotecaUserProperties
                        .getInstance().AdminId, sortingCode);

                if (result == true)
                {
                    Console.WriteLine("** La biblioteca ha sido ordenada correctamente **");
                }
                else
                {
                    Console.WriteLine("ERROR: No se ha podido ordenar la biblioteca");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
            }

        }

        public void executeOption3()
        {
            string isbn;
            string author;
            string title;
            int year;
            string country;
            string language;
            int available;

            Console.WriteLine("Introduce el ISBN: ");
            isbn = Console.ReadLine();

            Console.WriteLine("Introduce el autor: ");
            author = Console.ReadLine();

            Console.WriteLine("Introduce el título: ");
            title = Console.ReadLine();

            bool yearParsed = false;

            do
            {
                Console.WriteLine("Introduce el año:");
                yearParsed = Int32.TryParse(Console.ReadLine(), out year);

                if (!yearParsed)
                {
                    Console.WriteLine("Error: Introduzca un valor válido");
                }

            } while (!yearParsed);

            Console.WriteLine("Introduce el país:");
            country = Console.ReadLine();

            Console.WriteLine("Introduce el idioma: ");
            language = Console.ReadLine();

            bool availableParsed = false;

            do
            {
                Console.WriteLine("Introduce el número de libros inicial:");
                availableParsed = Int32.TryParse(Console.ReadLine(), out available);

                if (!availableParsed)
                {
                    Console.WriteLine("Error: Introduzca un valor válido");
                }

            } while (!availableParsed);

            TLibro book = new TLibro(title, author, country, language, isbn, year,
                    available, 0, 0);

            try
            {
                int numberOfRepositories = gestorBiblioteca.NRepositorios(
                        GestorBibliotecaUserProperties.getInstance().AdminId);

                if (numberOfRepositories == -1)
                {
                    Console.WriteLine("ERROR: Ya hay otro usuario identificado como"
                            + "administrador");

                    return;
                }

                if (numberOfRepositories == 0)
                {
                    Console.WriteLine("ERROR: No hay repositorios cargados en la biblioteca");
                    return;
                }

                List<TDatosRepositorio> repositories = new List<TDatosRepositorio>();

                for (int i = 0; i < numberOfRepositories; i++)
                {
                    TDatosRepositorio repository = gestorBiblioteca.DatosRepositorio(
                            GestorBibliotecaUserProperties.getInstance().AdminId, i);

                    if (repository != null)
                    {
                        repositories.Add(repository);
                    }
                }

                TDatosRepositorioUtils.showRepositoriesList(repositories);

                bool repositoryPositionParsed = false;
                int repositoryPosition;

                do
                {
                    Console.WriteLine("Elige repositorio: ");
                    repositoryPositionParsed = Int32.TryParse(Console.ReadLine(), out repositoryPosition);

                    if (!repositoryPositionParsed)
                    {
                        Console.WriteLine("Error: Introduzca un valor válido");
                    }

                } while (!repositoryPositionParsed);

                int result = gestorBiblioteca.NuevoLibro(GestorBibliotecaUserProperties.getInstance().AdminId,
                        book, (repositoryPosition - 1));

                switch (result)
                {
                    case -1:
                        Console.WriteLine("ERROR: Ya hay un usuario identificado como "
                                + "administrador");
                        break;
                    case -2:
                        Console.WriteLine("ERROR: El repositorio indicado no existe");
                        break;
                    case 0:
                        Console.WriteLine("ERROR: El libro ya existe dentro de la biblioteca");
                        break;
                    case 1:
                        Console.WriteLine("** El libro ha sido añadido correctamente **");
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
            }
        }

        public void executeOption4()
        {
            string isbn = "";

            Console.WriteLine("Introduce ISBN a buscar: ");
            isbn = Console.ReadLine();

            try
            {
                int result = gestorBiblioteca.Buscar(GestorBibliotecaUserProperties.getInstance()
                        .AdminId, isbn);

                switch (result)
                {
                    case -2:
                        Console.WriteLine("ERROR: Ya hay un usuario identificado como"
                                + "administrador");
                        break;
                    case -1:
                        Console.WriteLine("ERROR: No se ha encontrado ningún libro con"
                                + " el ISBN indicado");
                        break;
                    default:
                        TLibro book = gestorBiblioteca.Descargar(GestorBibliotecaUserProperties.getInstance()
                                .AdminId, -1, result);

                        if (book == null)
                        {
                            Console.WriteLine("ERROR: El libro no existe");
                            return;
                        }

                        BookUtils bookUtils = new BookUtils();
                        bookUtils.Mostrar(result, true, book);

                        string option = "";

                        do
                        {
                            Console.WriteLine("¿Es este el libro del que deseas comprar"
                                + " más unidades? (s/n)");
                            option = Console.ReadLine().ToLowerInvariant();

                            if (option != "s" && option != "n")
                            {
                                Console.WriteLine("ERROR: Opción incorrecta, indique"
                                        + "sí o no");
                            }
                        } while (option != "s" && option != "n");

                        if (option == "s")
                        {
                            int boughtBooks = 0;
                            bool boughtBooksParsed = false;

                            do
                            {
                                Console.WriteLine("Introduce el número de libros comprados:");
                                boughtBooksParsed = Int32.TryParse(Console.ReadLine(), out boughtBooks);

                                if (!boughtBooksParsed || boughtBooks < 1)
                                {
                                    Console.WriteLine("Error: Indique un número válido");
                                }

                            } while (!boughtBooksParsed || boughtBooks < 1);

                            int buyResult = gestorBiblioteca.Comprar(GestorBibliotecaUserProperties.getInstance()
                                .AdminId, isbn, boughtBooks);

                            if (buyResult == -1)
                            {
                                Console.WriteLine("ERROR: Ya hay un usuario identificado como"
                                + "administrador");
                            }
                            else if (buyResult == 0)
                            {
                                Console.WriteLine("ERROR: No se ha encontrado ningún libro con"
                                + " el ISBN indicado");
                            }
                            else if (buyResult == 1)
                            {
                                Console.WriteLine("** Se han añadido los nuevos libros **");
                            }
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
            }
        }

        public void executeOption5()
        {
            string isbn = "";

            Console.WriteLine("Introduce ISBN a buscar: ");
            isbn = Console.ReadLine();

            try
            {
                int result = gestorBiblioteca.Buscar(GestorBibliotecaUserProperties.getInstance()
                        .AdminId, isbn);

                switch (result)
                {
                    case -2:
                        Console.WriteLine("ERROR: Ya hay un usuario identificado como"
                                + "administrador");
                        break;
                    case -1:
                        Console.WriteLine("ERROR: No se ha encontrado ningún libro con"
                                + " el ISBN indicado");
                        break;
                    default:
                        TLibro book = gestorBiblioteca.Descargar(GestorBibliotecaUserProperties.getInstance()
                                .AdminId, -1, result);

                        if (book == null)
                        {
                            Console.WriteLine("ERROR: El libro no existe");
                            return;
                        }

                        BookUtils bookUtils = new BookUtils();
                        bookUtils.Mostrar(result, true, book);

                        string option = "";

                        do
                        {
                            Console.WriteLine("¿Es este el libro del que deseas retirar"
                                + " unidades? (s/n)");
                            option = Console.ReadLine().ToLowerInvariant();

                            if (option != "s" && option != "n")
                            {
                                Console.WriteLine("ERROR: Opción incorrecta, indique"
                                        + "sí o no");
                            }
                        } while (option != "s" && option != "n");

                        if (option == "s")
                        {
                            int removedBooks = 0;
                            bool removedBooksParsed = false;

                            do
                            {
                                Console.WriteLine("Introduce el número de libros a retirar:");
                                removedBooksParsed = Int32.TryParse(Console.ReadLine(), out removedBooks);

                                if (!removedBooksParsed || removedBooks < 1)
                                {
                                    Console.WriteLine("Error: introduzca un número válido");
                                }

                            } while (!removedBooksParsed || removedBooks < 1);


                            int removeResult = gestorBiblioteca.Retirar(GestorBibliotecaUserProperties.getInstance()
                                .AdminId, isbn, removedBooks);

                            if (removeResult == -1)
                            {
                                Console.WriteLine("ERROR: Ya hay un usuario identificado como"
                                + "administrador");
                            }
                            else if (removeResult == 0)
                            {
                                Console.WriteLine("ERROR: No se ha encontrado ningún libro con"
                                + " el ISBN indicado");
                            }
                            else if (removeResult == 1)
                            {
                                Console.WriteLine("** Se han retirado el número de libros indicados **");
                            }
                            else if (removeResult == 2)
                            {
                                Console.WriteLine("ERROR: El número de libros retirados excede los disponibles");
                            }
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
            }
        }

        public void executeOption7()
        {
            string searchText = "";

            Console.WriteLine("Introduce el texto a buscar: ");
            searchText = Console.ReadLine();

            string searchCode = "";

            Console.WriteLine("Código de Búsqueda");
            Console.WriteLine("I.- Por ISBN");
            Console.WriteLine("T.- Por Título");
            Console.WriteLine("A.- Por Autor");
            Console.WriteLine("P.- Por País");
            Console.WriteLine("D.- Por Idioma");
            Console.WriteLine("*.- Por todos los campos");

            do
            {
                Console.WriteLine("Introduce código: ");
                searchCode = Console.ReadLine();

                if (searchCode.ToLowerInvariant() != "i" && searchCode.ToLowerInvariant() != "t"
                        && searchCode.ToLowerInvariant() != "a" && searchCode.ToLowerInvariant() != "p"
                        && searchCode.ToLowerInvariant() != "d" && searchCode != "*")
                {
                    Console.WriteLine("ERORR: El código introducido no es válido");
                }
            } while (searchCode.ToLowerInvariant() != "i" && searchCode.ToLowerInvariant() != "t"
                        && searchCode.ToLowerInvariant() != "a" && searchCode.ToLowerInvariant() != "p"
                        && searchCode.ToLowerInvariant() != "d" && searchCode != "*");

            try
            {
                int numberOfRepositories = gestorBiblioteca.NRepositorios(
                        GestorBibliotecaUserProperties.getInstance().AdminId);

                if (numberOfRepositories == -1)
                {
                    Console.WriteLine("ERROR: Ya hay otro usuario identificado como"
                            + "administrador");

                    return;
                }

                if (numberOfRepositories == 0)
                {
                    Console.WriteLine("ERROR: No hay repositorios cargados en la biblioteca");
                    return;
                }

                List<TDatosRepositorio> repositories = new List<TDatosRepositorio>();

                for (int i = 0; i < numberOfRepositories; i++)
                {
                    TDatosRepositorio repository = gestorBiblioteca.DatosRepositorio(
                            GestorBibliotecaUserProperties.getInstance().AdminId,
                            i);

                    if (repository != null)
                    {
                        repositories.Add(repository);
                    }
                }

                TDatosRepositorioUtils.showRepositoriesListWithAllOption(repositories);

                Console.WriteLine("Elige repositorio: ");
                int repositoryPosition;
                Int32.TryParse(Console.ReadLine(), out repositoryPosition);

                repositoryPosition--;

                BookUtils bookUtils = new BookUtils();

                if (repositoryPosition == -1)
                {
                    int adminId = GestorBibliotecaUserProperties.getInstance().AdminId;

                    int totalNumberOfBooks = gestorBiblioteca.NLibros(-1);

                    List<TLibro> allBooks = new List<TLibro>();

                    for (int i = 0; i < totalNumberOfBooks; i++)
                    {
                        TLibro book = gestorBiblioteca.Descargar(adminId, -1, i);

                        if (book != null)
                        {
                            allBooks.Add(book);
                        }
                    }

                    bool headerShow = true;

                    searchCode = searchCode.ToLowerInvariant();

                    if (searchCode == BookSearcher.ISBN_SEARCH_FIELD)
                    {
                        for (int i = 0; i < allBooks.Count(); i++)
                        {
                            TLibro book = allBooks.ElementAt(i);

                            if (book.Isbn.Contains(searchText))
                            {
                                bookUtils.Mostrar(i, headerShow, book);
                                headerShow = false;
                            }
                        }
                    }
                    else if (searchCode == BookSearcher.TITLE_SEARCH_FIELD)
                    {
                        for (int i = 0; i < allBooks.Count(); i++)
                        {
                            TLibro book = allBooks.ElementAt(i);

                            if (book.Titulo.Contains(searchText))
                            {
                                bookUtils.Mostrar(i, headerShow, book);
                                headerShow = false;
                            }
                        }
                    }
                    else if (searchCode == BookSearcher.AUTHOR_SEARCH_FIELD)
                    {
                        for (int i = 0; i < allBooks.Count(); i++)
                        {
                            TLibro book = allBooks.ElementAt(i);

                            if (book.Autor.Contains(searchText))
                            {
                                bookUtils.Mostrar(i, headerShow, book);
                                headerShow = false;
                            }
                        }
                    }
                    else if (searchCode == BookSearcher.COUNTRY_SEARCH_FIELD)
                    {
                        for (int i = 0; i < allBooks.Count(); i++)
                        {
                            TLibro book = allBooks.ElementAt(i);

                            if (book.Pais.Contains(searchText))
                            {
                                bookUtils.Mostrar(i, headerShow, book);
                                headerShow = false;
                            }
                        }
                    }
                    else if (searchCode == BookSearcher.LANGUAGE_SEARCH_FIELD)
                    {
                        for (int i = 0; i < allBooks.Count(); i++)
                        {
                            TLibro book = allBooks.ElementAt(i);

                            if (book.Idioma.Contains(searchText))
                            {
                                bookUtils.Mostrar(i, headerShow, book);
                                headerShow = false;
                            }
                        }
                    }
                    else if (searchCode == BookSearcher.ALL_SEARCH_FIELD)
                    {
                        for (int i = 0; i < allBooks.Count(); i++)
                        {
                            TLibro book = allBooks.ElementAt(i);

                            if (book.Isbn.Contains(searchText) || book.Titulo.Contains(searchText)
                                    || book.Autor.Contains(searchText) || book.Pais.Contains(searchText)
                                    || book.Idioma.Contains(searchText))
                            {
                                bookUtils.Mostrar(i, headerShow, book);
                                headerShow = false;
                            }
                        }
                    }

                    //This means that no book has been found meeting the criteria
                    if (headerShow == true)
                    {
                        Console.WriteLine("Error: No se ha encontrado ningún libro");
                    }
                }
                else
                {
                    TDatosRepositorio repository = repositories.ElementAt(repositoryPosition);

                    List<TLibro> foundBooks = repository.BookRepository
                            .GetBooksBy(new BookSearcher(searchCode, searchText));

                    if (foundBooks.Count == 0)
                    {
                        Console.WriteLine("ERROR: No hay ningun libro que coincida con la busqueda");
                    }

                    for (int i = 0; i < foundBooks.Count(); i++)
                    {
                        TLibro book = foundBooks.ElementAt(i);

                        if (i == 0)
                        {
                            bookUtils.Mostrar(i, true, book);
                        }
                        else
                        {
                            bookUtils.Mostrar(i, false, book);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        public void executeOption8()
        {
            int adminId = GestorBibliotecaUserProperties.getInstance().AdminId;

            BookUtils bookUtils = new BookUtils();

            try
            {
                int totalNumberOfBooks = gestorBiblioteca.NLibros(-1);

                Console.WriteLine("Número total de libros: " + totalNumberOfBooks);

                for (int i = 0; i < totalNumberOfBooks; i++)
                {
                    TLibro book = gestorBiblioteca.Descargar(adminId, -1, i);

                    if (book != null)
                    {
                        if (i == 0)
                        {
                            bookUtils.Mostrar(i, true, book);
                        }
                        else
                        {
                            bookUtils.Mostrar(i, false, book);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
    }
}
