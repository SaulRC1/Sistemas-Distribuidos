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
                    //executeOption2();
                    break;
                case 3:
                    //executeOption3();
                    break;
                case 4:
                    //executeOption4();
                    break;
                case 5:
                    //executeOption5();
                    break;
                case 6:
                    executeOption6();
                    break;
                case 7:
                    //executeOption7();
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

                    if(book != null)
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
