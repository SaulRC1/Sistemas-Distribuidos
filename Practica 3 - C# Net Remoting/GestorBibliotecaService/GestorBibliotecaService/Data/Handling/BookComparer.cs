using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService.Data.Handling
{
    public class BookComparer : IComparer<TLibro>
    {
        public const int ISBN_SORTING_FIELD = 0;
        public const int TITLE_SORTING_FIELD = 1;
        public const int AUTHOR_SORTING_FIELD = 2;
        public const int YEAR_SORTING_FIELD = 3;
        public const int COUNTRY_SORTING_FIELD = 4;
        public const int LANGUAGE_SORTING_FIELD = 5;
        public const int AVAILABLE_SORTING_FIELD = 6;
        public const int BORROWED_SORTING_FIELD = 7;
        public const int BOOKED_SORTING_FIELD = 8;

        private readonly int sortingField;

        public BookComparer(int sortingField)
        {
            this.sortingField = sortingField;
        }

        public int Compare(TLibro x, TLibro y)
        {
            int C = 0;
            switch (sortingField)
            {
                case 0:
                    C = x.Isbn.CompareTo(y.Isbn);
                    break;
                case 1:
                    C = x.Titulo.CompareTo(y.Titulo);
                    break;
                case 2:
                    C = x.Autor.CompareTo(y.Autor);
                    break;
                case 3:
                    C = x.Anio.CompareTo(y.Anio);
                    break;
                case 4:
                    C = x.Pais.CompareTo(y.Pais);
                    break;
                case 5:
                    C = x.Idioma.CompareTo(y.Idioma);
                    break;
                case 6:
                    C = x.Disponibles.CompareTo(y.Disponibles);
                    break;
                case 7:
                    C = x.Prestados.CompareTo(y.Prestados);
                    break;
                case 8:
                    C = x.Reservados.CompareTo(y.Reservados);
                    break;
            }

            return C;
        }
    }
}
