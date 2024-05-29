using GestorBibliotecaClient.GestorBibliotecaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaClient.Util
{
    public class TDatosRepositorioUtils
    {
        private static String Ajustar(String S, int Ancho)
        {
            byte[] v = Encoding.ASCII.GetBytes(S);
            int c = 0;
            int len = 0;
            uint uin;
            for (int i = 0; i < v.Length; i++)
            {
                uin = Convert.ToUInt32(v[i]); //.toUnsignedInt(v[i]);
                if (uin > 128)
                {
                    c++;
                }
            }

            len = c / 2;

            for (int i = 0; i < len; i++)
            {
                S = S + " ";
            }

            return S;
        }

        public static void showRepositoriesList(List<TDatosRepositorio> repositories)
        {
            Console.WriteLine(String.Format("{0,-5}{1,-38}{2,-28}{3,-10}", "POS", "NOMBRE", "DIRECCION", "Nº LIBROS"));

            for (int i = 0; i < 93; i++)
            {
                Console.Write("*");
            }

            Console.WriteLine("");

            for (int i = 0; i < repositories.Count(); i++)
            {
                TDatosRepositorio repository = repositories.ElementAt(i);

                String nombre = Ajustar(String.Format("{0,-38}", repository.RepositoryName), 38);

                Console.WriteLine(String.Format("{0,-5}{1,-18}{2,-28}{3,-10}",
                    (i + 1), nombre, repository.RepositoryAddress, repository.NumberOfBooks));
            }
        }

        public static void showRepositoriesListWithAllOption(List<TDatosRepositorio> repositories)
        {
            Console.WriteLine(String.Format("{0,-5}{1,-38}{2,-28}{3,-10}", "POS", "NOMBRE", "DIRECCION", "Nº LIBROS"));

            for (int i = 0; i < 93; i++)
            {
                Console.Write("*");
            }

            Console.WriteLine("");

            for (int i = 0; i < repositories.Count(); i++)
            {
                TDatosRepositorio repository = repositories.ElementAt(i);

                String nombre = Ajustar(String.Format("{0,-38}", repository.RepositoryName), 38);

                Console.WriteLine(String.Format("{0,-5}{1,-18}{2,-28}{3,-10}",
                    (i + 1), nombre, repository.RepositoryAddress, repository.NumberOfBooks));
            }

            Console.WriteLine(String.Format("{0,-5}{1,-18}", 0, "Todos los repositorios"));
        }
    }
}
