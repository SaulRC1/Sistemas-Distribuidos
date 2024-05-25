using GestorBibliotecaService.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaService
{
    public class GestorBibliotecaService : MarshalByRefObject, GestorBibliotecario_inf
    {
        private int adminId = -1;

        private const string adminPassword = "1234";

        private List<TDatosRepositorio> loadedRepositories = new List<TDatosRepositorio>();

        private int sortingField = 0;

        private List<TLibro> generalBookStorage = new List<TLibro>();

        public int AbrirRepositorio(int pIda, string pNomFichero)
        {
            if(pIda != adminId)
            {
                return -1;
            }

            FileStream fs = null;
            BinaryReader binaryReader = null;

            try
            {
                fs = File.Open(pNomFichero, FileMode.Open);

                binaryReader = new BinaryReader(fs, Encoding.UTF8, false);

                int numberOfBooks = binaryReader.ReadInt32();
                string repositoryName = binaryReader.ReadString();
                string repositoryAddress = binaryReader.ReadString();

                BookRepository bookRepository = new InMemoryBookRepository();

                TDatosRepositorio repositoryData = new TDatosRepositorio(repositoryName,
                    repositoryAddress, numberOfBooks, bookRepository, pNomFichero);

                if(loadedRepositories.Contains(repositoryData))
                {
                    return -2;
                }

                for (int i = 0; i < numberOfBooks; i++)
                {
                    string isbn = binaryReader.ReadString();
                    string title = binaryReader.ReadString();
                    string author = binaryReader.ReadString();
                    int year = binaryReader.ReadInt32();
                    string country = binaryReader.ReadString();
                    string language = binaryReader.ReadString();
                    int available = binaryReader.ReadInt32();
                    int borrowed = binaryReader.ReadInt32();
                    int booked = binaryReader.ReadInt32();

                    TLibro book = new TLibro(title, author, country, language, isbn, year, available, borrowed, booked);

                    repositoryData.BookRepository.AddBook(book);
                    generalBookStorage.Add(book);
                }

                loadedRepositories.Add(repositoryData);

                Ordenar(pIda, sortingField);

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }

                if (binaryReader != null)
                {
                    binaryReader.Close();
                    binaryReader.Dispose();
                }
            }
            
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
            if(pRepo != -1 && pRepo >= this.loadedRepositories.Count())
            {
                return null;
            }

            if(pRepo == -1)
            {
                if(pPos < 0 || pPos >= this.generalBookStorage.Count())
                {
                    return null;
                }

                TLibro generalBook = (TLibro) this.generalBookStorage.ElementAt(pPos).Clone();
                
                if (pIda != adminId)
                {
                    generalBook.Prestados = 0;
                    generalBook.Reservados = 0;
                }

                return generalBook;
            }

            TDatosRepositorio repositoryData = this.loadedRepositories.ElementAt(pRepo);

            if(pPos < 0 || pPos >= repositoryData.BookRepository.GetNumberOfBooks())
            {
                return null;
            }

            TLibro book = (TLibro) repositoryData.BookRepository.GetAllBooks().ElementAt(pPos).Clone();

            if(pIda != adminId)
            {
                book.Prestados = 0;
                book.Reservados = 0;
            }

            return book;
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
            if(pRepo != -1 && pRepo >= this.loadedRepositories.Count())
            {
                return -1;
            }

            if(pRepo == -1)
            {
                int numberOfBooks = 0;

                foreach (TDatosRepositorio repositoryData in loadedRepositories)
                {
                    numberOfBooks += repositoryData.BookRepository.GetNumberOfBooks();
                }

                return numberOfBooks;
            }

            return this.loadedRepositories.ElementAt(pRepo).BookRepository.GetNumberOfBooks();
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
            return true;
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
