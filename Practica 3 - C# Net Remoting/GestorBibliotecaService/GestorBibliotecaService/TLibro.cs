using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService
{
    [Serializable]
    public class TLibro : IEquatable<TLibro>, ICloneable
    {
        private string titulo;
        private string autor;
        private string pais;
        private string idioma;
        private string isbn;
        private int anio;

        private int disponibles;
        private int prestados;
        private int reservados;

        public TLibro()
        {
        }

        public TLibro(string titulo, string autor, string pais, string idioma,
                string isbn, int anio, int disponibles, int prestados,
                int reservados)
        {
            this.titulo = titulo;
            this.autor = autor;
            this.pais = pais;
            this.idioma = idioma;
            this.isbn = isbn;
            this.anio = anio;
            this.disponibles = disponibles;
            this.prestados = prestados;
            this.reservados = reservados;
        }

        public string Titulo
        {
            get { return this.titulo; }
            set { this.titulo = value; }
        }

        public string Autor
        {
            get { return this.autor; }
            set { this.autor = value; }
        }

        public string Pais
        {
            get { return this.pais; }
            set { this.pais = value; }
        }

        public string Idioma
        {
            get { return this.idioma; }
            set { this.idioma = value; }
        }

        public string Isbn
        {
            get { return this.isbn; }
            set { this.isbn = value; }
        }

        public int Anio
        {
            get { return this.anio; }
            set { this.anio = value; }
        }

        public int Disponibles
        {
            get { return this.disponibles; }
            set { this.disponibles = value; }
        }

        public int Prestados
        {
            get { return this.prestados; }
            set { this.prestados = value; }
        }

        public int Reservados
        {
            get { return this.reservados; }
            set { this.reservados = value; }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TLibro);
        }

        public bool Equals(TLibro other)
        {
            return other != null &&
                   isbn == other.isbn;
        }

        public override int GetHashCode()
        {
            return 1553653001 + EqualityComparer<string>.Default.GetHashCode(isbn);
        }

        public static bool operator ==(TLibro left, TLibro right)
        {
            return EqualityComparer<TLibro>.Default.Equals(left, right);
        }

        public static bool operator !=(TLibro left, TLibro right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            string s = "{" + this.titulo + ", " + this.autor + ", " + this.anio + ", "
                + this.pais + ", " + this.idioma + ", " + this.isbn + ", " + this.disponibles
                + ", " + this.prestados + ", " + this.reservados + "}";

            return s;
        }
    }
}
