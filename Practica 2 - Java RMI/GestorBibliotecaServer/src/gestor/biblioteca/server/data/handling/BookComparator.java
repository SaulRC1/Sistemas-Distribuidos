package gestor.biblioteca.server.data.handling;

import gestor.biblioteca.service.models.TLibro;
import java.util.Comparator;

/**
 *
 * @author Saúl Rodríguez Naranjo
 */
public class BookComparator implements Comparator<TLibro>
{
    public static final int ISBN_SORTING_FIELD = 0;
    public static final int TITLE_SORTING_FIELD = 1;
    public static final int AUTHOR_SORTING_FIELD = 2;
    public static final int YEAR_SORTING_FIELD = 3;
    public static final int COUNTRY_SORTING_FIELD = 4;
    public static final int LANGUAGE_SORTING_FIELD = 5;
    public static final int AVAILABLE_SORTING_FIELD = 6;
    public static final int BORROWED_SORTING_FIELD = 7;
    public static final int BOOKED_SORTING_FIELD = 8;
    
    private final int sortingField;

    public BookComparator(int sortingField)
    {
        this.sortingField = sortingField;
    }

    @Override
    public int compare(TLibro o1, TLibro o2)
    {
        int C = 0;
        switch (sortingField)
        {
            case 0:
                C = o1.getIsbn().compareTo(o2.getIsbn());
                break;
            case 1:
                C = o1.getTitulo().compareTo(o2.getTitulo());
                break;
            case 2:
                C = o1.getAutor().compareTo(o2.getAutor());
                break;
            case 3:
                C = Integer.compare(o1.getAnio(), o2.getAnio());
                break;
            case 4:
                C = o1.getPais().compareTo(o2.getPais());
                break;
            case 5:
                C = o1.getIdioma().compareTo(o2.getIdioma());
                break;
            case 6:
                C = Integer.compare(o1.getDisponibles(), o2.getDisponibles());
                break;
            case 7:
                C = Integer.compare(o1.getPrestados(), o2.getPrestados());
                break;
            case 8:
                C = Integer.compare(o1.getReservados(), o2.getReservados());
                break;
        }
        return C;
    }

}
