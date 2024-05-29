using GestorBibliotecaClient.GestorBibliotecaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaClient.Util
{
    public class BookUtils
    {
        private String Ajustar(String S, int Ancho)
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


        public void Mostrar(int Pos, bool Cabecera, TLibro libro)
        {
            if (Cabecera)
            {
                Console.WriteLine(String.Format("{0,-5}{1,-58}{2,-18}{3,-4}{4,-4}{5,-4}", "POS", "TITULO", "ISBN", "DIS", "PRE", "RES"));
                Console.WriteLine(String.Format("     {0,-30}{1,-28}{2,-12}", "AUTOR", "PAIS (IDIOMA)", "AÑO"));
                for (int i = 0; i < 93; i++)
                {
                    Console.Write("*");
                }
                Console.WriteLine("\n");
            }

            String T = Ajustar(String.Format("{0,-58}", libro.Titulo), 58);
            String A = Ajustar(String.Format("{0,-30}", libro.Autor), 30);
            String PI = Ajustar(String.Format("{0,-28}", libro.Pais + " (" + libro.Idioma + ")"), 28);

            Console.WriteLine(String.Format("{0,-5}{1,-18}{2,-4}{3,4}{4,4}{5,4}", Pos + 1, T, libro.Isbn, libro.Disponibles, libro.Prestados, libro.Reservados));
            Console.WriteLine(String.Format("     {0}{1}{2,-12}", A, PI, libro.Anio));
        }
    }
}
