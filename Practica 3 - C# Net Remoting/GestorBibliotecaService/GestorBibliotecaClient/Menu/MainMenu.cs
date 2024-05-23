using GestorBibliotecaService.UserProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService.Menu
{
    public class MainMenu
    {
        public const string MAIN_MENU_TITLE = "GESTOR BIBLIOTECARIO 2.0 (M.PRINCIPAL)";
        public const string OPTION_0_TITLE = "0.- Salir";
        public const string OPTION_1_TITLE = "1.- M. Administración";
        public const string OPTION_2_TITLE = "2.- Consulta de libros";
        public const string OPTION_3_TITLE = "3.- Préstamo de libros";
        public const string OPTION_4_TITLE = "4.- Devolución de libros";

        private GestorBibliotecaService gestorBiblioteca;

        public MainMenu(GestorBibliotecaService gestorBiblioteca)
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
                default:
                    Console.WriteLine("opción incorrecta");
                    break;
            }
        }
    }
}
