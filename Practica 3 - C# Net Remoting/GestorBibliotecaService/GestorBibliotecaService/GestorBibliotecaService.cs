using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService
{
    public class GestorBibliotecaService : MarshalByRefObject, GestorBibliotecario_inf
    {
        private int adminId = -1;

        private const string adminPassword = "1234";

        public int AbrirRepositorio(int pIda, string pNomFichero)
        {
            throw new NotImplementedException();
        }

        public int Buscar(int pIda, string pIsbn)
        {
            throw new NotImplementedException();
        }

        public int Comprar(int pIda, string pIsbn, int pNoLibros)
        {
            throw new NotImplementedException();
        }

        public int Conexion(string pPasswd)
        {
            if (adminId >= 1)
            {
                //There is and admin identified already
                return -1;
            }

            if (pPasswd != adminPassword)
            {
                //Wrong password
                return -2;
            }

            Random random = new Random();

            //random number between 1 and 1.000.000
            adminId = random.Next(1, 1000001);

            return adminId;
        }

        public TDatosRepositorio DatosRepositorio(int pIda, int pRepo)
        {
            throw new NotImplementedException();
        }

        public TLibro Descargar(int pIda, int pRepo, int pPos)
        {
            throw new NotImplementedException();
        }

        public bool Desconexion(int pIda)
        {
            if (pIda != adminId)
            {
                return false;
            }

            adminId = -1;

            return true;
        }

        public int Devolver(int pPos)
        {
            throw new NotImplementedException();
        }

        public int GuardarRepositorio(int pIda, int pRepo)
        {
            throw new NotImplementedException();
        }

        public int NLibros(int pRepo)
        {
            throw new NotImplementedException();
        }

        public int NRepositorios(int pIda)
        {
            throw new NotImplementedException();
        }

        public int NuevoLibro(int pIda, TLibro L, int pRepo)
        {
            throw new NotImplementedException();
        }

        public bool Ordenar(int pIda, int pCampo)
        {
            throw new NotImplementedException();
        }

        public int Prestar(int pPos)
        {
            throw new NotImplementedException();
        }

        public int Retirar(int pIda, string pIsbn, int pNoLibros)
        {
            throw new NotImplementedException();
        }
    }
}
