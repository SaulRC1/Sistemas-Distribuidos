﻿using GestorBibliotecaService.UserProperties;
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
                    //executeOption1();
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
                    //executeOption6();
                    break;
                case 7:
                    //executeOption7();
                    break;
                case 8:
                    //executeOption8();
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
    }
}
