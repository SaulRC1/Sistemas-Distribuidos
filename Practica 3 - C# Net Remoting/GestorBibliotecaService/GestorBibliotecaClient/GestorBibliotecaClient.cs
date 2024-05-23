using GestorBibliotecaService.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService
{
    class GestorBibliotecaClient
    {
        static void Main(string[] args)
        {
            string host;
            int port;
            bool parsingResult = false;

            Console.WriteLine("Indique host al que quiere conectarse:");
            host = Console.ReadLine();

            do
            {
                Console.WriteLine("Indique puerto del host:");
                parsingResult = Int32.TryParse(Console.ReadLine(), out port);

                if(!parsingResult)
                {
                    Console.WriteLine("Por favor, indique un puerto válido.");
                }
            } while (!parsingResult);

            try
            {
                ChannelServices.RegisterChannel(new TcpChannel(), false);

                GestorBibliotecaService gestorBiblioteca = (GestorBibliotecaService)Activator.GetObject(typeof(GestorBibliotecaService),
                    "tcp://" + host + ":" + port + "/GestorBiblioteca");

                int option = -1;

                MainMenu mainMenu = new MainMenu(gestorBiblioteca);

                do
                {
                    mainMenu.showMainMenu();
                    Int32.TryParse(Console.ReadLine(), out option);

                    mainMenu.executeOption(option);

                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
            
        }
    }
}
