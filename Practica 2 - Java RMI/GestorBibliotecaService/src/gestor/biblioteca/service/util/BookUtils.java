package gestor.biblioteca.service.util;

import gestor.biblioteca.service.models.TLibro;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public class BookUtils
{

    private String Ajustar(String S, int Ancho)
    {
        byte v[] = S.getBytes();
        int c = 0;
        int len = 0;
        int uin;
        for (int i = 0; i < v.length; i++)
        {
            uin = Byte.toUnsignedInt(v[i]);
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

    
    public void Mostrar(int Pos, boolean Cabecera, TLibro libro)
    {
        if (Cabecera)
        {
            System.out.println(String.format("%-5s%-58s%-18s%-4s%-4s%-4s", "POS", "TITULO", "ISBN", "DIS", "PRE", "RES"));
            System.out.println(String.format("     %-30s%-28s%-12s", "AUTOR", "PAIS (IDIOMA)", "AÑO"));
            for (int i = 0; i < 93; i++)
            {
                System.out.print("*");
            }
            System.out.print("\n");
        }

        String T = Ajustar(String.format("%-58s", libro.getTitulo()), 58);
        String A = Ajustar(String.format("%-30s", libro.getAutor()), 30);
        String PI = Ajustar(String.format("%-28s", libro.getPais() + " (" + libro.getIdioma() + ")"), 28);

        System.out.println(String.format("%-5d%s%-18s%-4d%-4d%-4d", Pos + 1, T, libro.getIsbn(), libro.getDisponibles(), libro.getPrestados(), libro.getReservados()));
        System.out.println(String.format("     %s%s%-12d", A, PI, libro.getAnio()));
    }
}
