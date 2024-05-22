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
            ChannelServices.RegisterChannel(new TcpChannel(9000), false);
            Console.WriteLine("Registrando el servicio de Gestor Bibliotecario en modo Singleton...");
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(GestorBibliotecaService), "GestorBiblioteca",
                WellKnownObjectMode.Singleton);

            Console.WriteLine("Esperando llamadas remotas...");
            Console.WriteLine("Pulsa enter para salir...");
            Console.ReadLine();
        }
    }
}
