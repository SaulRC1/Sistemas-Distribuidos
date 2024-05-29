using GestorBibliotecaService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService
{
    [DataContract]
    public class TDatosRepositorio : IEquatable<TDatosRepositorio>
    {
        private string repositoryName;
        private string repositoryAddress;
        private int numberOfBooks;
        private string repositoryFilePath;

        private BookRepository bookRepository;

        public TDatosRepositorio()
        {
        }

        public TDatosRepositorio(string repositoryName, string repositoryAddress, int numberOfBooks,
                BookRepository bookRepository, string repositoryFilePath)
        {
            this.repositoryName = repositoryName;
            this.repositoryAddress = repositoryAddress;
            this.numberOfBooks = numberOfBooks;
            this.bookRepository = bookRepository;
            this.repositoryFilePath = repositoryFilePath;
        }

        [DataMember]
        public string RepositoryName
        {
            get { return this.repositoryName; }
            set { this.repositoryName = value; }
        }

        [DataMember]
        public string RepositoryAddress
        {
            get { return this.repositoryAddress; }
            set { this.repositoryAddress = value; }
        }

        [DataMember]
        public int NumberOfBooks
        {
            get { return this.numberOfBooks; }
            set { this.numberOfBooks = value; }
        }

        [IgnoreDataMember]
        public BookRepository BookRepository
        {
            get { return this.bookRepository; }
            set { this.bookRepository = value; }
        }

        [IgnoreDataMember]
        public string RepositoryFilePath
        {
            get { return this.repositoryFilePath; }
            set { this.repositoryFilePath = value; }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TDatosRepositorio);
        }

        public bool Equals(TDatosRepositorio other)
        {
            return other != null &&
                   repositoryName == other.repositoryName &&
                   repositoryAddress == other.repositoryAddress;
        }

        public override int GetHashCode()
        {
            int hashCode = -1138466103;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(repositoryName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(repositoryAddress);
            return hashCode;
        }
    }
}
