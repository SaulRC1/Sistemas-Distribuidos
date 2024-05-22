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
            ChannelServices.RegisterChannel(new TcpChannel(), false);

            GestorBibliotecaService gestorBiblioteca = (GestorBibliotecaService)Activator.GetObject(typeof(GestorBibliotecaService),
                "tcp://localhost:9000/GestorBiblioteca");



        }
    }
}
