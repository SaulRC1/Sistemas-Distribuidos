using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService
{
    class GestorBibliotecaServer
    {
        static void Main(string[] args)
        {
            int port;
            bool parsingResult = false;

            do
            {
                Console.WriteLine("Indica puerto TCP para iniciar el servidor: ");
                parsingResult = Int32.TryParse(Console.ReadLine(), out port);

                if (!parsingResult)
                {
                    Console.WriteLine("Por favor, indique un puerto válido.");
                }
            } while (!parsingResult);

            try
            {
                ChannelServices.RegisterChannel(new TcpChannel(port), false);
                Console.WriteLine("Registrando el servicio de Gestor Bibliotecario en modo Singleton...");
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(GestorBibliotecaService), "GestorBiblioteca",
                    WellKnownObjectMode.Singleton);

                Console.WriteLine("Esperando llamadas remotas...");
                Console.WriteLine("Pulsa enter para salir...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
            
        }
    }
}
