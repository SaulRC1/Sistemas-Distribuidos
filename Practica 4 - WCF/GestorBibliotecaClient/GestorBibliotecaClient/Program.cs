using GestorBibliotecaClient.GestorBibliotecaService;
using GestorBibliotecaClient.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GestorBibliotecaService.GestorBibliotecaClient gestorBiblioteca = new GestorBibliotecaService.GestorBibliotecaClient();

                int option = -1;

                MainMenu mainMenu = new MainMenu(gestorBiblioteca);

                do
                {
                    mainMenu.showMainMenu();

                    Console.WriteLine("Selecciona una opción:");
                    Int32.TryParse(Console.ReadLine(), out option);

                    mainMenu.executeOption(option);

                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
    }
}
