﻿using GestorBibliotecaClient.GestorBibliotecaService;
using GestorBibliotecaClient.Util;
using GestorBibliotecaService.UserProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaClient.Menu
{
    public class MainMenu
    {
        public const string MAIN_MENU_TITLE = "GESTOR BIBLIOTECARIO 2.0 (M.PRINCIPAL)";
        public const string OPTION_0_TITLE = "0.- Salir";
        public const string OPTION_1_TITLE = "1.- M. Administración";
        public const string OPTION_2_TITLE = "2.- Consulta de libros";
        public const string OPTION_3_TITLE = "3.- Préstamo de libros";
        public const string OPTION_4_TITLE = "4.- Devolución de libros";

        private GestorBibliotecaService.GestorBibliotecaClient gestorBiblioteca;

        public MainMenu(GestorBibliotecaService.GestorBibliotecaClient gestorBiblioteca)
        {
            this.gestorBiblioteca = gestorBiblioteca;
        }

        public void showMainMenu()
        {
            Console.WriteLine(MAIN_MENU_TITLE);
            Console.WriteLine("*******************************");
            Console.WriteLine(OPTION_1_TITLE);
            Console.WriteLine(OPTION_2_TITLE);
            Console.WriteLine(OPTION_3_TITLE);
            Console.WriteLine(OPTION_4_TITLE);
            Console.WriteLine(OPTION_0_TITLE);
        }

        public void executeOption0()
        {
            System.Environment.Exit(0);
        }

        public void executeOption1()
        {
            try
            {
                String password = "";

                Console.WriteLine("Por favor inserte la contraseña de administración:");
                password = Console.ReadLine();

                int connectionResult = gestorBiblioteca.Conexion(password);

                switch (connectionResult)
                {
                    case -1:
                        Console.WriteLine("ERROR: Ya hay un usuario identificado como administrador");
                        break;
                    case -2:
                        Console.WriteLine("ERROR: Contraseña errónea");
                        break;
                    default:
                        GestorBibliotecaUserProperties.getInstance().AdminId = connectionResult;

                        AdministrationMenu adminMenu = new AdministrationMenu(gestorBiblioteca);

                        int option = -1;

                        do
                        {
                            adminMenu.show();

                            Console.WriteLine("Selecciona una opción:");
                            Int32.TryParse(Console.ReadLine(), out option);

                            adminMenu.executeOption(option);

                        } while (option != 0);

                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de conexión: " + ex.ToString());
            }
        }

        public void executeOption2()
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
                BookUtils bookUtils = new BookUtils();

                int adminId = GestorBibliotecaUserProperties.getInstance().AdminId;

                int totalNumberOfBooks = gestorBiblioteca.NLibros(-1);

                if (totalNumberOfBooks <= 0)
                {
                    Console.WriteLine("ERROR: No hay repositorios cargados en la biblioteca");
                    return;
                }

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

                if (searchCode == "i")
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
                else if (searchCode == "t")
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
                else if (searchCode == "a")
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
                else if (searchCode == "p")
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
                else if (searchCode == "d")
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
                else if (searchCode == "*")
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
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        public void executeOption3()
        {
            executeOption2();

            string option = "";

            do
            {
                Console.WriteLine("¿Quieres sacar algún libro de la biblioteca? (s/n)");
                option = Console.ReadLine().ToLowerInvariant();

                if (option != "s" && option != "n")
                {
                    Console.WriteLine("ERROR: Opción inválida. Por favor, indique "
                            + "sí o no");
                }

            } while (option != "s" && option != "n");

            if (option == "s")
            {
                int bookPosition = -1;
                bool bookPositionParsed = false;

                do
                {
                    Console.WriteLine("Introduce la posición del libro a solicitar su préstamo:");
                    bookPositionParsed = Int32.TryParse(Console.ReadLine(), out bookPosition);

                    if (!bookPositionParsed)
                    {
                        Console.WriteLine("Error: Introduzca una posición válida");
                    }

                } while (!bookPositionParsed);

                try
                {
                    int result = gestorBiblioteca.Prestar((bookPosition - 1));

                    switch (result)
                    {
                        case -1:
                            Console.WriteLine("ERROR: La posición indicada no está dentro"
                                    + " de los límites del repositorio mezclado y ordenado");
                            break;
                        case 1:
                            Console.WriteLine("** El préstamo se ha concedido, recoja el"
                                    + " libro en el mostrador **");
                            break;
                        case 0:
                            Console.WriteLine("** Se le ha puesto en lista de espera **");
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
        }

        public void executeOption4()
        {
            executeOption2();

            string option = "";

            do
            {
                Console.WriteLine("¿Quieres devolver algún libro de la biblioteca? (s/n)");
                option = Console.ReadLine().ToLowerInvariant();

                if (option != "s" && option != "n")
                {
                    Console.WriteLine("ERROR: Opción inválida. Por favor, indique "
                            + "sí o no");
                }

            } while (option != "s" && option != "n");

            if (option == "s")
            {
                int bookPosition = -1;
                bool bookPositionParsed = false;

                do
                {
                    Console.WriteLine("Introduce la posición del libro a devolver:");
                    bookPositionParsed = Int32.TryParse(Console.ReadLine(), out bookPosition);

                    if (!bookPositionParsed)
                    {
                        Console.WriteLine("Error: introduzca un valor válido");
                    }

                } while (!bookPositionParsed);

                try
                {
                    int result = gestorBiblioteca.Devolver((bookPosition - 1));

                    switch (result)
                    {
                        case -1:
                            Console.WriteLine("ERROR: La posición indicada no está dentro"
                                    + " de los límites del repositorio mezclado y ordenado");
                            break;
                        case 0:
                            Console.WriteLine("** Se ha devuelto el libro y se "
                                    + "ha reducido el número de usuarios en espera **");
                            break;
                        case 1:
                            Console.WriteLine("** Se ha devuelto el libro y se "
                                    + "pondrá en la estantería **");
                            break;
                        case 2:
                            Console.WriteLine("ERROR: El libro no se puede devolver, "
                                    + "porque no hay usuarios en lista de espera ni "
                                    + "libros prestados");
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
                    Console.WriteLine("opción incorrecta");
                    break;
            }
        }
    }
}
